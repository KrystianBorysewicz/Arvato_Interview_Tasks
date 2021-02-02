using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure
{
    public interface IExchangeClient
    {
        Task<IEnumerable<CurrencyRate>> GetRates();

        /// <summary>
        /// Retrieves current currency rates.
        /// </summary>
        /// <returns>Current currency rates.</returns>
        Task<IEnumerable<CurrencyRate>> GetRatesAsync(string baseCurrency);

        /// <summary>
        /// Retrieves historical currency rates.
        /// </summary>
        /// <param name="date">The date from which the rates should be retrieved.</param>
        /// <returns>Historical currency rates.</returns>
        Task<IEnumerable<CurrencyRate>> GetRatesAsync(string baseCurrency, DateTime date);

        Task<IEnumerable<CurrencyRate>> GetRatesAsync(DateTime date);

        

        
    }
}
