using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface ICurrencyRatesService
    {
        /// <summary>
        /// Stores a single <see cref="CurrencyRate"/>.
        /// </summary>
        /// <param name="currencyRate"></param>
        /// <returns>The stored <see cref="CurrencyRate"/>.</returns>
        Task<CurrencyRate> StoreCurrencyRate(CurrencyRate currencyRate);

        /// <summary>
        /// Stores multiple <see cref="CurrencyRate"/>s.
        /// </summary>
        /// <param name="currencyRate"></param>
        /// <returns>The stored <see cref="CurrencyRate"/>s.</returns>
        Task<IEnumerable<CurrencyRate>> StoreCurrencyRates(IEnumerable<CurrencyRate> currencyRate);

        /// <summary>
        /// Retreives and stores current <see cref="CurrencyRate"/>s.
        /// </summary>
        /// <param name="currencyRate"></param>
        /// <returns>The stored <see cref="CurrencyRate"/>s.</returns>
        Task<IEnumerable<CurrencyRate>> StoreCurrencyRates();
    }
}