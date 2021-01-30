﻿using Application;
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
                new SqlServerConnectionFactory(
                    @"Database=LeechBA;Data Source=(LocalDb)\VitaCafe;Integrated Security=SSPI;"));

            // Add scoped Exchange Client
            services.AddScoped<IExchangeClient, FixerClient>(x =>
                new FixerClient("ApiKey123?"));

            _serviceProvider = services.BuildServiceProvider(true);

            await ExchangeFunctions.RunConverter();

        }

        async void RunConverter()
        {
            Console.Write("Input a date for the rates in the format DD-MM-YYYY or continue to use latest rates: ");

            var dateString = Console.ReadLine();
            var date = new DateTime();
            if (!string.IsNullOrWhiteSpace(dateString))
            {
                while (!DateTime.TryParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {

                    Console.WriteLine("Your date was incorrect, please try again: ");
                    dateString = Console.ReadLine();
                }
            }
            var rates = date == DateTime.MinValue ? await _client.GetRates() : await _client.GetRates(date);

            Console.Write("Input Currency Code 1:");
            var Symbol1 = Console.ReadLine();

                Console.WriteLine($"{Symbol1} is not a valid currency. Please try again: ");
                Symbol1 = Console.ReadLine();

            Console.Write("Input Currency Code 2:");
            var Symbol2 = Console.ReadLine();

                Console.WriteLine($"{Symbol2} is not a valid currency. Please try again: ");
                Symbol2 = Console.ReadLine();

            Console.Write("Input Currency 1 Amount:");

            // TODO: Needs proper validation of amount.
            var amount = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            // TOCHECK: Could use rounding?
            Console.WriteLine($"{amount} {Symbol1} = {rates[Symbol2] / rates[Symbol1] * amount} {Symbol2}");
        }
    }

}
