namespace ECommerce.Dtos.Product;

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public List<string> ImageUrls { get; set; } = null!;
    public List<int> CategoryIds { get; set; } = null!;
}
public class ProductResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } =string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public List<string> ImageUrls { get; set; } = null!;
    public List<string> Categories { get; set; } = null!;
}