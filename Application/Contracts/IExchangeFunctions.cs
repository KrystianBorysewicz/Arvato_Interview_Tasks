using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IExchangeFunctions
    {
        Task<decimal> ConvertCurrencyAsync(string initialSymbol, string convertToSymbol, decimal amount);
        Task<decimal> ConvertCurrencyAsync(string initialSymbol, string convertToSymbol, decimal amount, DateTime date);
        Task<CurrencyRate> GetCurrencyRateAsync(string symbol1, string convertToSymbol, decimal amount);
        Task<CurrencyRate> GetCurrencyRateAsync(string symbol1, string convertToSymbol, decimal amount, DateTime date);
    }
}
