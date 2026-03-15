using ECommerce.Common;

namespace ECommerce.Models;

public class ProductCategory:BaseEntity
{
    public Product Product { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
