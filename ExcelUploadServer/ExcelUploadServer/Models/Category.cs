﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelUploadServer.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
