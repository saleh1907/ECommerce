using ECommerce.Common;

namespace ECommerce.Models;

public class ProductImage:BaseEntity
{
    public string ImageUrl { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

}
