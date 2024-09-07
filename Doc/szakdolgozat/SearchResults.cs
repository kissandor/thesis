public class SearchResult
{
    public int Id { get; set; }
    public int ComputerPartId { get; set; }
    public ComputerPart ComputerPart { get; set; } = null!;
    public string Description { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public double? ComputerPartPrice { get; set; }
    public string? Currency { get; set; }
    public int WebShopId { get; set; }
    public  WebShop WebShop { get; set; } = null!;
    public string ProductUrl { get; set; } = null!;
}