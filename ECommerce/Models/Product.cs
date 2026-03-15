using ECommerce.Common;

namespace ECommerce.Models;

public class Product:BaseEntity
{
    public String Name { get; set; } = string.Empty;
    public String Description { get; set; }=string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public List<ProductImage> Images { get; set; } = null!;
    public List<ProductCategory> ProductCategories { get; set; } = null!;


}
