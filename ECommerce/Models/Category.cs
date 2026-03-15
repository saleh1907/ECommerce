using ECommerce.Common;

namespace ECommerce.Models;

public class Category:BaseEntity
{
    public string Name { get; set; }= string.Empty;

    public List<ProductCategory> ProductCategories { get; set; } = null!;
}
