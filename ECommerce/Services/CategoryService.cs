using ECommerce.Contexts;
using ECommerce.Dtos.Category;
using ECommerce.Models;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services;

public class CategoryService:ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _context.Categories
            .Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();

        return categories;
    }

    public async Task<CategoryResponseDto> GetByIdAsync(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
            throw new Exception("Kateqoriya tapilmadi");

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<string> CreateAsync(CreateCategoryDto dto)
    {
        var exists = await _context.Categories
            .AnyAsync(c => c.Name.ToLower() == dto.Name.ToLower());

        if (exists)
            throw new Exception("Bu adda kateqoriya artıq movcuddur");

        var category = new Category
        {
            Name = dto.Name
        };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return "Kateqoriya yaradildi.";
    }

    public async Task<string> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
            throw new Exception("Kateqoriya tapilmadi");

        var exists = await _context.Categories
            .AnyAsync(c => c.Id != id && c.Name.ToLower() == dto.Name.ToLower());

        if (exists)
            throw new Exception("Bu adda basqa kateqoriya artiq movcuddur");

        category.Name = dto.Name;
        await _context.SaveChangesAsync();

        return "Kateqoriya  yenilendi";
    }

    public async Task<string> DeleteAsync(int id)
    {
        var category = await _context.Categories
            .Include(c => c.ProductCategories)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
            throw new Exception("Kateqoriya tapilmadi");

        if (category.ProductCategories.Any())
            throw new Exception("Bu kateqoriya mehsullara baqlıdır, siline bilmez.");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return "Kateqoriya  silindi.";
    }
}
