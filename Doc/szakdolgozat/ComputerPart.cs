  public class ComputerPart
    {
        public int Id { get; set; }
        public string ComputerPartName { get; set; } = null!;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ComputerPartPrice { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; } = null!;
        public int? WebshopId { get; set; }
        public WebShop? Webshop { get; set; }
    }

