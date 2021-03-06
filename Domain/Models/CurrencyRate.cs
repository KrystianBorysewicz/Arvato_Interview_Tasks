﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CurrencyRate
    {
        public CurrencyRate(string symbol, decimal value, string comparedSymbol, DateTime date)
        {
            Symbol = symbol;
            Value = value;
            ComparedSymbol = comparedSymbol;
            Date = date;
        }

        public string Symbol { get; set; }

        public decimal Value { get; set; }

        public string ComparedSymbol { get; set; }

        public DateTime Date { get; set; }
    }
}
