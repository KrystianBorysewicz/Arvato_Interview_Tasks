using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArvatoInterviewTasks.ExternalAPIs.Fixer
{
    public class FixerClient : IExchangeClient
    {
        static string BaseURL = "http://data.fixer.io/api/";
        
        string ApiKey;

        public FixerClient(string apiKey)
        {
            ApiKey = apiKey;
        }

        /// <inheritdoc/>
        public async Task<Dictionary<string, double>> GetRates()
        {

            var rates = await CallAsync<Dictionary<string, double>>("rates", Endpoints.Latest, true);

            return rates;
        }

        /// <inheritdoc/>
        public async Task<Dictionary<string, double>> GetRates(DateTime date)
        {

            var rates = await CallAsync<Dictionary<string, double>>("rates", date.ToString("yyyy-mm-dd"), true);

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
        /// Calls a HTTP request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="authorized"></param>
        /// <returns>A specific property from the root response object.</returns>
        protected async Task<T> CallAsync<T>(string JsonProperty, string endpoint, bool authorized = false)
        {
            using (var client = new HttpClient())
            {
                var url = UrlBuilder(endpoint, authorized);
                HttpResponseMessage response = await client.GetAsync(url);
                var data = await response.Content.ReadAsStringAsync();
                return JObject.Parse(data)[JsonProperty].ToObject<T>();
            }
        }

        string UrlBuilder(string endpoint, bool authorized)
        {
            return $"{BaseURL}{endpoint}{(authorized ? $"?access_key={ApiKey}" : "")}";
        }
    }
    
}
