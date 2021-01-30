using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IExchangeFunctions
    {
        Task<CurrencyRate> ConvertCurrency(string symbol1, string symbol2, double amount, DateTime date = new DateTime());
    }
}
