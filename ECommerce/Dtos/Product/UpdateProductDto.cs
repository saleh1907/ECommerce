namespace ECommerce.Dtos.Product;

public class UpdateProductDto
{ 
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public List<int> CategoryIds { get; set; } = new();
}
