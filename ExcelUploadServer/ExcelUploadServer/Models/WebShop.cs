using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelUploadServer.Models
{
    public class WebShop
    {
        public int Id { get; set; }
        public string WebShopName { get; set; } = null!;
        public string WebShopURL { get; set; } = null!;
    }
}
