using ECommerce.Dtos.Product;

namespace ECommerce.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductResponseDto>> GetAllAsync();
    Task<ProductResponseDto> GetByIdAsync(int id);
    Task<string> CreateAsync(CreateProductDto dto);
    Task<string> UpdateAsync(int id, UpdateProductDto dto);
    Task<string> DeleteAsync(int id);
}


