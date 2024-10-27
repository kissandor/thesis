using System;
using System.Activities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ekke.Thesis.GetCheapestProduct.Activities.Properties;
using Ekke.Thesis.GetCheapestProduct.Models;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace Ekke.Thesis.GetCheapestProduct.Activities
{
    [LocalizedDisplayName(nameof(Resources.GetCheapestProduct_DisplayName))]
    [LocalizedDescription(nameof(Resources.GetCheapestProduct_Description))]
    public class GetCheapestProduct : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetCheapestProduct_ConnectionString_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetCheapestProduct_ConnectionString_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ConnectionString { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetCheapestProduct_Products_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetCheapestProduct_Products_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<List<Product>> Products { get; set; }

        #endregion


        #region Constructors

        public GetCheapestProduct()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (ConnectionString == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ConnectionString)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            try
            {
                // Inputs
                var connectionString = ConnectionString.Get(context);
                DatabaseHelper db = new DatabaseHelper(connectionString);
                List<Product> cheapestProduc = db.GetCheapestProductsFromDatabase();


                // Outputs
                return (ctx) =>
                {
                    Products.Set(ctx, cheapestProduc);
                };
            }
            catch (Exception ex) 
            {
                return (ctx) =>
                {
                    Products.Set(ctx, null);
                };
            }
        }

        #endregion
    }
}

