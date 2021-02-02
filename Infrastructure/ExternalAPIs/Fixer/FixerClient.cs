using Application.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Domain.Models;

namespace Infrastructure.ExternalAPIs.Fixer
{
    public class FixerClient : IExchangeClient
    {
        static string BaseURL = "http://data.fixer.io/api/";

        readonly string ApiKey;

        public FixerClient(string apiKey)
        {
            ApiKey = apiKey;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CurrencyRate>> GetRates()
        {
            var rates = new List<CurrencyRate>();
            var messageRoot = await CallAsync<RatesResponseMessage>(Endpoints.Latest, true);

            foreach(var rate in messageRoot.rates)
            {
                rates.Add(new CurrencyRate(rate.Key, rate.Value, messageRoot.Base, messageRoot.date));
            }
            return rates;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CurrencyRate>> GetRatesAsync(string baseCurrency, DateTime date)
        {
            var parameters = new Dictionary<string, object>() { { "base", baseCurrency } };
            var rates = await CallAsync<List<CurrencyRate>>("rates", date.ToString("yyyy-MM-dd"), true, parameters);

            return rates;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CurrencyRate>> GetRatesAsync(DateTime date)
        {
            var rates = new List<CurrencyRate>();
            var messageRoot = await CallAsync<RatesResponseMessage>(date.ToString("yyyy-MM-dd"), true);

            foreach (var x in messageRoot.rates)
            {
                rates.Add(new CurrencyRate(x.Key, x.Value, messageRoot.Base, messageRoot.date));
            }
            return rates;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CurrencyRate>> GetRatesAsync(string baseCurrency)
        {
            var parameters = new Dictionary<string, object>() { { "base", baseCurrency } };
            var rates = await CallAsync<List<CurrencyRate>>("rates", Endpoints.Latest, true, parameters);
            return rates;
        }

        /// <summary>
        /// Calls a REST request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="authorized"></param>
        /// <returns>The parsed root response object.</returns>
        protected async Task<T> CallAsync<T>(string endpoint, bool authorized = false)
        {
            using (var client = new HttpClient())
            {
                var url = UrlBuilder(endpoint, authorized);
                HttpResponseMessage response = await client.GetAsync(url);
                var data = await response.Content.ReadAsStringAsync();
                return JObject.Parse(data).ToObject<T>();
            }
        }

        /// <summary>
        /// Calls a REST request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="authorized"></param>
        /// <returns>The parsed root response object.</returns>
        protected async Task<T> CallAsync<T>(string endpoint, bool authorized = false, IDictionary<string, object> parameters = null)
        {
            using (var client = new HttpClient())
            {
                var url = UrlBuilder(endpoint, authorized, parameters);
                HttpResponseMessage response = await client.GetAsync(url);
                var data = await response.Content.ReadAsStringAsync();
                return JObject.Parse(data).ToObject<T>();
            }
        }

        /// <summary>
        /// Calls a HTTP request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="authorized"></param>
        /// <returns>A specific property from the root response object.</returns>
        protected async Task<T> CallAsync<T>(string JsonProperty, string endpoint, bool authorized = false, IDictionary<string, object> parameters = null)
        {
            using (var client = new HttpClient())
            {
                var url = UrlBuilder(endpoint, authorized, parameters);
                HttpResponseMessage response = await client.GetAsync(url);
                var data = await response.Content.ReadAsStringAsync();
                return JObject.Parse(data)[JsonProperty].ToObject<T>();
            }
        }

        string UrlBuilder(string endpoint, bool authorized, IDictionary<string, object> parameters = null)
        {
            string url = $"{BaseURL}{endpoint}{(authorized ? $"?access_key={ApiKey}" : "")}{(parameters == null ? "" : string.Join("", parameters.Select(x => $"&{x.Key}={x.Value}")))}";
            return url;
        }
    }

}
