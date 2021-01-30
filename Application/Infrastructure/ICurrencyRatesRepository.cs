using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Infrastructure
{
    public interface ICurrencyRatesRepository 
    {
        Task<CurrencyRate> AddCurrencyRates(CurrencyRate currencyRate, CancellationToken cancellationToken = default);

        Task<IEnumerable<CurrencyRate>> AddCurrencyRates(IEnumerable<CurrencyRate> currencyRate, CancellationToken cancellationToken = default);

    }
}
