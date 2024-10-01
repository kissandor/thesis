public class PriceFinder : CodeActivity
{
    [Category("Input")]
    public InArgument<string> InputText { get; set; }
    [Category("Output")]     
    public OutArgument<PriceCurrencyPair> PriceAndCurrency { get; set; }

    protected override void Execute(CodeActivityContext context)
    {
        try
        {
            string inputText = InputText.Get(context);
            if (string.IsNullOrWhiteSpace(inputText))
            {
                throw new ArgumentException();
            }

            PriceCurrencyPair pc = Finder(inputtext);

            PriceAndCurrency.Set(context, pc);
        }
        catch (Exception)
        {
            PriceAndCurrency.Set(context, null);
        }
    }
    private PriceCurrencyPair Finder(string searchForPrice)
    {
        // Implementation of Finder logic
       return new PriceCurrencyPair(price, currency);
    }
}
