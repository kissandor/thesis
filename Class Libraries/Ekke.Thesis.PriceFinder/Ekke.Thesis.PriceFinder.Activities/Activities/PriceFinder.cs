using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using Ekke.Thesis.PriceFinder.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;
using Ekke.Thesis.PriceFinder.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Ekke.Thesis.PriceFinder.Activities
{
    [LocalizedDisplayName(nameof(Resources.PriceFinder_DisplayName))]
    [LocalizedDescription(nameof(Resources.PriceFinder_Description))]
    public class PriceFinder : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.PriceFinder_InputText_DisplayName))]
        [LocalizedDescription(nameof(Resources.PriceFinder_InputText_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> InputText { get; set; }

        [LocalizedDisplayName(nameof(Resources.PriceFinder_PriceAndCurrency_DisplayName))]
        [LocalizedDescription(nameof(Resources.PriceFinder_PriceAndCurrency_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<PriceCurrencyPair> PriceAndCurrency { get; set; }

        #endregion


        #region Constructors

        public PriceFinder()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (InputText == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(InputText)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            try
            {
                // Inputs
                var inputtext = InputText.Get(context);
                if (string.IsNullOrWhiteSpace(inputtext))
                {
                    throw new ArgumentException();
                }

                
                PriceCurrencyPair pc = await FindPriceAsync(inputtext, cancellationToken);

                // Outputs
                return (ctx) => {
                    PriceAndCurrency.Set(ctx, pc);
                };
            }
            catch (Exception ex)
            {
                return (ctx) => {
                    PriceAndCurrency.Set(ctx, null);
                };
            }
        }
        #endregion

        #region Helper Methods
        private Task<PriceCurrencyPair> FindPriceAsync(string inputText, CancellationToken cancellationToken)
        {
            PriceCurrencyPair pcp = Finder(inputText);
            
            return Task.FromResult(pcp);
        }
       
        private PriceCurrencyPair Finder(string SerachForPrice) 
        {
            string inputWithoutSpaceInPrice = RemoveSpacesAfterIntegers(SerachForPrice);

            // Regular expression to match1 a price and currency pair, including thousands separators
            var regex1 = new Regex(@"([\d,]+(\.\d{1,3})?)\s*(USD|EUR|GBP|JPY|CAD|AUD|CHF|CNY|SEK|Ft|HUF|FT)", RegexOptions.IgnoreCase);
            var regex2 = new Regex(@"(\$|€|£|¥|CA\$|AU\$|USD|EUR|GBP|CHF|CNY|SEK|Ft|HUF|FT)?\s?([\d,]+(\.\d{1,3})?)", RegexOptions.IgnoreCase);
            var match1 = regex1.Match(inputWithoutSpaceInPrice);
            var match2 = regex2.Match(inputWithoutSpaceInPrice);

            if (match1.Success)
            {
                // Remove thousands separators before parsing the price
                string priceString = match1.Groups[1].Value.Replace(",", "");
                string trimmedPriceString = RemoveDecimalPointIfMoreThanTwoDigits(priceString);
                
                double price = double.Parse(trimmedPriceString, CultureInfo.InvariantCulture);
                string currency = match1.Groups[3].Value.ToUpper();
                if(!string.IsNullOrEmpty(currency) && price > 0)
                {
                    return new PriceCurrencyPair(price, currency);
                }
                return null;
            }
            else if (match2.Success)
            {
                // Extract and clean price string
                string priceString = match2.Groups[2].Value.Replace(",", "");
                string trimmedPriceString  =RemoveDecimalPointIfMoreThanTwoDigits(priceString);
                
                double price = double.Parse(trimmedPriceString, CultureInfo.InvariantCulture);

                // Extract currency symbol or code
                string currency = match2.Groups[1].Value.ToUpper().Replace("CA$", "CAD").Replace("AU$", "AUD");

                if (!string.IsNullOrEmpty(currency) && price > 0)
                {
                    return new PriceCurrencyPair(price, currency);
                }
                return null;
            }

            return null; // Return null if no price-currency pair is found
        }

        public static string RemoveSpacesAfterIntegers(string s)
        {
            // Use regex to find a digit followed by a space and replace it with just the digit
            return Regex.Replace(s, @"(\d) ", "$1");
        }

        public static string RemoveDecimalPointIfMoreThanTwoDigits(string input)
        {
            var regex = new Regex(@"\.(\d{3,})"); // Olyan mintát keres, ahol a pontot 3 vagy több számjegy követ
            return regex.IsMatch(input) ? input.Replace(".", "") : input; 
        }

        #endregion
    }
}

