using Application.Contracts;
using Application.Infrastructure;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Application
{
    public class ExchangeFunctions : IExchangeFunctions
    {
        public static IServiceProvider serviceProvider;
        private readonly IExchangeClient _client;
        public ExchangeFunctions(IExchangeClient client) 
        {
            _client = client;
        }

        public async Task<decimal> ConvertCurrencyAsync(string initialSymbol, string convertToSymbol, decimal amount)
        {
            return await ConvertCurrencyInternalAsync(initialSymbol, convertToSymbol, amount, null);
        }

        public async Task<decimal> ConvertCurrencyAsync(string initialSymbol, string convertToSymbol, decimal amount, DateTime date)
        {
            return await ConvertCurrencyInternalAsync(initialSymbol, convertToSymbol, amount, date);
        }

        async Task<decimal> ConvertCurrencyInternalAsync(string initialSymbol, string convertToSymbol, decimal amount, DateTime? date)
        {
            var rates = date.HasValue
                ? await _client.GetRatesAsync(date.Value)
                : await _client.GetRates();

            var initialSymbolRate = rates.FirstOrDefault(rate => rate.Symbol == initialSymbol);
            var convertToSymbolRate = rates.FirstOrDefault(rate => rate.Symbol == convertToSymbol);
            var convertedValue = convertToSymbolRate.Value / initialSymbolRate.Value * amount;
            return convertedValue;
        }

        public async Task<CurrencyRate> GetCurrencyRateAsync(string symbol1, string symbol2, decimal amount)
        {
            var rates = await _client.GetRates();
            var currencyRate = rates.First(x => x.Symbol == symbol1);
            return currencyRate;
        }

        public async Task<CurrencyRate> GetCurrencyRateAsync(string symbol1, string symbol2, decimal amount, DateTime date)
        {
            var rates = await _client.GetRatesAsync(date);
            var currencyRate = rates.First(x => x.Symbol == symbol1);
            return currencyRate;
        }
    }
}
