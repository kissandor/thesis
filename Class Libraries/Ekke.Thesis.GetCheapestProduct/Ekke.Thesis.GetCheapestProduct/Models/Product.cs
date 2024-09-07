using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekke.Thesis.GetCheapestProduct.Models
{
    public class Product
    {
        public string Category { get; set; }
        public string ComputerPartName { get; set; }
        public string Description { get; set; }
        public double ComputerPartPrice { get; set; }
        public string Currency { get; set; }
        public string Webshop { get; set; }
        public string ProductUrl { get; set; }

    }
}
