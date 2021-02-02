using Application;
using Application.Contracts;
using Application.Infrastructure;
using Infrastructure.ExternalAPIs.Fixer;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Configuration;

namespace ArvatoInterviewTasks
{
    class Program
    {
        static IServiceProvider _serviceProvider;
        static async Task Main(string[] args)
        {
            // Add scoped DB Connection
            var services = new ServiceCollection();
            services.AddScoped<IConnectionFactory, SqlServerConnectionFactory>(x =>
                new SqlServerConnectionFactory(ConfigurationManager.AppSettings.Get("SqlConnectionString")));

            // Add scoped Exchange Client
            services.AddScoped<IExchangeClient, FixerClient>(x =>
                new FixerClient(ConfigurationManager.AppSettings.Get("FixerApiKey")));

            services.AddScoped<IExchangeFunctions, ExchangeFunctions>();

            _serviceProvider = services.BuildServiceProvider(true);

            await RunConverterAsync();

        }

        static async Task RunConverterAsync()
        {
            Console.Write("Input a date for the rates in the format DD-MM-YYYY or continue to use latest rates: ");
            var date = new DateTime();
            var dateString = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dateString))
            {
                while (!DateTime.TryParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {

                    Console.WriteLine("Your date was incorrect, please try again: ");
                    dateString = Console.ReadLine();
                }
            }

            Console.Write("Input Currency Code 1:");
            var Symbol1 = Console.ReadLine();

            Console.Write("Input Currency Code 2:");
            var Symbol2 = Console.ReadLine();

            Console.Write("Input Currency 1 Amount:");

            // TODO: Needs proper validation of amount.
            var amount = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            using (var scope = _serviceProvider.CreateScope())
            {
                var exchangeFunctions = scope.ServiceProvider.GetService<IExchangeFunctions>();
                var convertedValue = string.IsNullOrWhiteSpace(dateString) 
                    ? await exchangeFunctions.ConvertCurrencyAsync(Symbol1, Symbol2, amount) 
                    : await exchangeFunctions.ConvertCurrencyAsync(Symbol1, Symbol2, amount, date);

                // TOCHECK: Could use rounding?
                Console.WriteLine($"{amount} {Symbol1} = {convertedValue} {Symbol2}");
            }

        }
    }

}
