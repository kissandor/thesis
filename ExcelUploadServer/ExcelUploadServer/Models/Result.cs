using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelUploadServer.Models
{
    [Keyless]
    public class SearchResult
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string ComputerPartName { get; set; } = null!;
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ComputerPartPrice { get; set; }
        public string? Currency { get; set; } = null!;

        public int WebshopId { get; set; }
        public virtual WebShop Webshop { get; set; } = null!;
    }
}
