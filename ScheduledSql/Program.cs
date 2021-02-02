using Application;
using Application.Contracts;
using Application.Infrastructure;
using Infrastructure.ExternalAPIs.Fixer;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ScheduledSql
{
    class Program
    {
        static IServiceProvider _serviceProvider;
        static async Task Main(string[] args)
        {
            InitializeDependencyInjection();

            await ScheduledFunction();

        }
        private static async Task ScheduledFunction()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
               
                var _currencyRateService = scope.ServiceProvider.GetService<ICurrencyRatesService>();

                await _currencyRateService.StoreCurrencyRates();
            }
        }
        private static void InitializeDependencyInjection()
        {
            // Add scoped DB Connection
            var services = new ServiceCollection();
            services.AddScoped<IConnectionFactory, SqlServerConnectionFactory>(x =>
                new SqlServerConnectionFactory(ConfigurationManager.AppSettings.Get("SqlConnectionString")));

            // Add scoped Exchange Client
            services.AddScoped<IExchangeClient, FixerClient>(x =>
                new FixerClient(ConfigurationManager.AppSettings.Get("FixerApiKey")));

            services.AddScoped<IExchangeFunctions, ExchangeFunctions>();

            services.AddScoped<ICurrencyRatesService, CurrencyRatesService>();

            services.AddScoped<ICurrencyRatesRepository, CurrencyRatesRepository>();

            _serviceProvider = services.BuildServiceProvider(true);
        }
    }
}
