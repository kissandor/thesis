using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using Ekke.Thesis.GetCheapestProduct.Activities.Design.Designers;
using Ekke.Thesis.GetCheapestProduct.Activities.Design.Properties;

namespace Ekke.Thesis.GetCheapestProduct.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            var categoryAttribute = new CategoryAttribute($"{Resources.Category}");

            builder.AddCustomAttributes(typeof(GetCheapestProduct), categoryAttribute);
            builder.AddCustomAttributes(typeof(GetCheapestProduct), new DesignerAttribute(typeof(GetCheapestProductDesigner)));
            builder.AddCustomAttributes(typeof(GetCheapestProduct), new HelpKeywordAttribute(""));


            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
