using ECommerce.Dtos.Category;

namespace ECommerce.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto> GetByIdAsync(int id);
    Task<string> CreateAsync(CreateCategoryDto dto);
    Task<string> UpdateAsync(int id, UpdateCategoryDto dto);
    Task<string> DeleteAsync(int id);
}
