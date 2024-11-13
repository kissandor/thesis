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