using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekke.Thesis.PriceFinder.Models
{
    public class PriceCurrencyPair
    {
        public double Price { get; set; }
        public string Currency { get; set; }

        public PriceCurrencyPair(double price, string currency)
        {
            Price = price;
            Currency = currency;
        }
    }
}
