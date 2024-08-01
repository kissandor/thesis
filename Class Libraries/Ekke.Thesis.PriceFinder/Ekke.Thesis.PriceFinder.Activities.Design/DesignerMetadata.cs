using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using Ekke.Thesis.PriceFinder.Activities.Design.Designers;
using Ekke.Thesis.PriceFinder.Activities.Design.Properties;

namespace Ekke.Thesis.PriceFinder.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            var categoryAttribute = new CategoryAttribute($"{Resources.Category}");

            builder.AddCustomAttributes(typeof(PriceFinder), categoryAttribute);
            builder.AddCustomAttributes(typeof(PriceFinder), new DesignerAttribute(typeof(PriceFinderDesigner)));
            builder.AddCustomAttributes(typeof(PriceFinder), new HelpKeywordAttribute(""));


            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
