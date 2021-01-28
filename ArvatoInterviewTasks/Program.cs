using ArvatoInterviewTasks.ExternalAPIs;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArvatoInterviewTasks
{
    class Program
    {
        static IExchangeClient _client;
        static async Task Main(string[] args)
        {
            _client = new ExternalAPIs.Fixer.FixerClient("apiKey");

            await RunConverter();

        }

        static async Task RunConverter()
        {
            var rates = await _client.GetRates();

            Console.Write("Input Currency Code 1:");
            var Symbol1 = Console.ReadLine();

            while(!rates.ContainsKey(Symbol1))
            {
                Console.WriteLine($"{Symbol1} is not a valid currency. Please try again: ");
                Symbol1 = Console.ReadLine();
            }

            Console.Write("Input Currency Code 2:");
            var Symbol2 = Console.ReadLine();

            while (!rates.ContainsKey(Symbol2))
            {
                Console.WriteLine($"{Symbol2} is not a valid currency. Please try again: ");
                Symbol2 = Console.ReadLine();
            }

            Console.Write("Input Currency 1 Amount:");

            // TODO: Needs proper validation of amount.
            var amount = double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);

            // TOCHECK: Could use rounding?
            Console.WriteLine($"{amount} {Symbol1} = {rates[Symbol2] / rates[Symbol1] * amount} {Symbol2}");
        }

    }
}
