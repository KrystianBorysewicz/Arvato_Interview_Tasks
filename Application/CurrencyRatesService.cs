using Application.Contracts;
using Application.Infrastructure;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private readonly IExchangeClient exchangeClient;
        private readonly ICurrencyRatesRepository currencyRatesRepository;

        public CurrencyRatesService(IExchangeClient exchangeClient, ICurrencyRatesRepository currencyRatesRepository)
        {
            this.exchangeClient = exchangeClient ?? throw new ArgumentNullException(nameof(exchangeClient));
            this.currencyRatesRepository = currencyRatesRepository ?? throw new ArgumentNullException(nameof(currencyRatesRepository));
        }

        public Task<CurrencyRate> StoreCurrencyRate(CurrencyRate currencyRate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CurrencyRate>> StoreCurrencyRates()
        {
            var rates = await exchangeClient.GetRates();

            var addedRates = await currencyRatesRepository.AddCurrencyRates(rates);

            return addedRates;
        }

        public async Task<IEnumerable<CurrencyRate>> StoreCurrencyRates(IEnumerable<CurrencyRate> rates)
        {
            var addedRates = await currencyRatesRepository.AddCurrencyRates(rates);

            return addedRates;
        }
    }
}
