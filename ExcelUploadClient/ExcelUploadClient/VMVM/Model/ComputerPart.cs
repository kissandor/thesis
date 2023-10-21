using ExcelUploadClient.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploadClient.VMVM.Model
{
    public class ComputerPart
    {
        public int Id { get; set; }
        public string ComputerPartName { get; set; }
        public decimal? ComputerPartPrice { get; set; }
        public int CategoryId { get; set; }
        public int? WebshopId { get; set; }
  
    }
}
