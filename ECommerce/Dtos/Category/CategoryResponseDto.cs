using ECommerce.Common;

namespace ECommerce.Dtos.Category;

public class CategoryResponseDto:BaseEntity
{
    public string Name { get; set; } =string.Empty;
}