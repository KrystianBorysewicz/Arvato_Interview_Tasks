using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArvatoInterviewTasks.ExternalAPIs
{
    interface IExchangeClient
    {
        /// <summary>
        /// Retrieves historical currency rates.
        /// </summary>
        /// <param name="date">The date from which the rates should be retrieved.</param>
        /// <returns>Historical currency rates.</returns>
        Task<Dictionary<string, double>> GetRates(DateTime date);

        /// <summary>
        /// Retrieves current currency rates.
        /// </summary>
        /// <returns>Current currency rates.</returns>
        Task<Dictionary<string, double>> GetRates();
    }
}
