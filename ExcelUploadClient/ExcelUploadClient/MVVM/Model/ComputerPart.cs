﻿using ExcelUploadClient.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploadClient.MVVM.Model
{
    public class ComputerPart
    {
        public int Id { get; set; }
        public string ComputerPartName { get; set; }

        public string CategoryName { get; set; }
        // public decimal? ComputerPartPrice { get; set; }
        //  public int CategoryId { get; set; }
        //  public int? WebshopId { get; set; }

    }
}
