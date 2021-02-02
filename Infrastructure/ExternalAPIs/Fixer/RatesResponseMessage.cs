using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Infrastructure.ExternalAPIs.Fixer
{
    class RatesResponseMessage
    {
        public bool Success { get; set; }
        public long Timestamp { get; set; }
        public bool Historical { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }
        public DateTime date { get; set; }
        public Dictionary<string, decimal> rates { get; set; }

    }
}
