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

        public async Task<double> ConvertCurrency(string symbol1, string symbol2, double amount)
        {
            var rates = await _client.GetRates(symbol2);
            var currencyRate = rates.First(x => x.Symbol == symbol1);
            var rate = amount * currencyRate.Value;
            return rate;
        }

        public async Task<double> ConvertCurrency(string symbol1, string symbol2, double amount, DateTime date)
        {
            var rates = await _client.GetRates(symbol2, date);
            var currencyRate = rates.First(x => x.Symbol == symbol1);
            var rate = amount * currencyRate.Value;
            return rate;
        }

        public async Task<CurrencyRate> GetCurrencyRate(string symbol1, string symbol2, double amount)
        {
            var rates = await _client.GetRates(symbol2);
            var currencyRate = rates.First(x => x.Symbol == symbol1);
            return currencyRate;
        }

        public async Task<CurrencyRate> GetCurrencyRate(string symbol1, string symbol2, double amount, DateTime date)
        {
            var rates = await _client.GetRates(symbol2, date);
            var currencyRate = rates.First(x => x.Symbol == symbol1);
            return currencyRate;
        }

        //public async Task RunConverter()
        //{
        //    Console.Write("Input a date for the rates in the format DD-MM-YYYY or continue to use latest rates: ");

        //    var dateString = Console.ReadLine();
        //    var date = new DateTime();
        //    if (!string.IsNullOrWhiteSpace(dateString))
        //    {
        //        while (!DateTime.TryParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        //        {

        //            Console.WriteLine("Your date was incorrect, please try again: ");
        //            dateString = Console.ReadLine();
        //        }
        //    }
        //    var rates = date == DateTime.MinValue ? await _client.GetRates() : await _client.GetRates(date);

        //    Console.Write("Input Currency Code 1:");
        //    var Symbol1 = Console.ReadLine();

        //    while (!rates.ContainsKey(Symbol1))
        //    {
        //        Console.WriteLine($"{Symbol1} is not a valid currency. Please try again: ");
        //        Symbol1 = Console.ReadLine();
        //    }

        //    Console.Write("Input Currency Code 2:");
        //    var Symbol2 = Console.ReadLine();

        //    while (!rates.ContainsKey(Symbol2))
        //    {
        //        Console.WriteLine($"{Symbol2} is not a valid currency. Please try again: ");
        //        Symbol2 = Console.ReadLine();
        //    }

        //    Console.Write("Input Currency 1 Amount:");

        //    // TODO: Needs proper validation of amount.
        //    var amount = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

        //    // TOCHECK: Could use rounding?
        //    Console.WriteLine($"{amount} {Symbol1} = {rates[Symbol2] / rates[Symbol1] * amount} {Symbol2}");
        //}
    }
}
