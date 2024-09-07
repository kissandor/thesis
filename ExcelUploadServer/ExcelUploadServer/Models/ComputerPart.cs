using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelUploadServer.Models
{
    public class ComputerPart
    {
        public int Id { get; set; }
        public string ComputerPartName { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
