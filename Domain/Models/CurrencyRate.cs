using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CurrencyRate
    {
        public string Symbol { get; set; }

        public double Value { get; set; }

        public string ComparedSymbol { get; set; }

        public DateTime Date { get; set; }
    }
}
