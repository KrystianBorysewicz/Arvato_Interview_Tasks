using Application.Infrastructure;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Infrastructure.Repositories
{
    public class CurrencyRatesRepository : ICurrencyRatesRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public CurrencyRatesRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /// <summary>
        /// Stores a <see cref="CurrencyRate"/>.
        /// </summary>
        /// <param name="currencyRate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CurrencyRate> AddCurrencyRates(CurrencyRate currencyRate, CancellationToken cancellationToken = default)
        {
            using IDbConnection conn = _connectionFactory.CreateConnection();
            conn.Open();
            string query = "INSERT INTO currency_rates (symbol, value, compared_symbol, date) VALUES (@Symbol, @Value, @ComparedSymbol, @Date)";
            await conn.ExecuteAsync(query, currencyRate);
            return currencyRate;
        }

        /// <summary>
        /// Stores <see cref="CurrencyRate"/>s.
        /// </summary>
        /// <param name="currencyRates"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CurrencyRate>> AddCurrencyRates(IEnumerable<CurrencyRate> currencyRates, CancellationToken cancellationToken = default)
        {
            using IDbConnection conn = _connectionFactory.CreateConnection();
            conn.Open();
            string query = "INSERT INTO currency_rates (symbol, value, compared_symbol, date) VALUES (@Symbol, @Value, @ComparedSymbol, @Date)";
            await conn.ExecuteAsync(query, currencyRates);
            return currencyRates;
        }
    }
}
