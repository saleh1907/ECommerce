using ECommerce.Contexts;
using ECommerce.Dtos.Product;
using ECommerce.Models;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services;

public class ProductService:IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var products = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
            .ToListAsync();

        return products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            ImageUrls = p.Images.Select(i => i.ImageUrl).ToList(),
            Categories = p.ProductCategories.Select(pc => pc.Category.Name).ToList()
        }).ToList();
    }

    public async Task<ProductResponseDto> GetByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            throw new KeyNotFoundException("Mehsul tapilmadi.");

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            ImageUrls = product.Images.Select(i => i.ImageUrl).ToList(),
            Categories = product.ProductCategories.Select(pc => pc.Category.Name).ToList()
        };
    }

    public async Task<string> CreateAsync(CreateProductDto dto)
    {
        if (dto.CategoryIds is null || !dto.CategoryIds.Any())
            throw new ArgumentException("Mehsul en azi bir kateqoriyaya aid olmalidir.");

        var categoriesCount = await _context.Categories
            .CountAsync(c => dto.CategoryIds.Contains(c.Id));

        if (categoriesCount != dto.CategoryIds.Distinct().Count())
            throw new ArgumentException("Gonderilen kateqoriyalardan biri ve ya bir necesi movcud deyil.");

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity
        };

        product.Images = dto.ImageUrls.Select(url => new ProductImage
        {
            ImageUrl = url
        }).ToList();

        product.ProductCategories = dto.CategoryIds.Distinct().Select(categoryId => new ProductCategory
        {
            CategoryId = categoryId
        }).ToList();

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return "Mehsul  yaradildi.";
    }

    public async Task<string> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            throw new KeyNotFoundException("Mehsul tapilmadi.");

        if (dto.CategoryIds is null || !dto.CategoryIds.Any())
            throw new ArgumentException("Mehsul en azi bir kateqoriyaya aid olmalidir.");



        var categoriesCount = await _context.Categories
            .CountAsync(c => dto.CategoryIds.Contains(c.Id));

        if (categoriesCount != dto.CategoryIds.Distinct().Count())
            throw new ArgumentException("Gonderilen kateqoriyalardan biri ve ya bir necesi movcud deyil.");


        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;

        _context.ProductImages.RemoveRange(product.Images);
        product.Images = dto.ImageUrls.Select(url => new ProductImage
        {
            ImageUrl = url,
            ProductId = product.Id
        }).ToList();

        _context.ProductCategories.RemoveRange(product.ProductCategories);
        product.ProductCategories = dto.CategoryIds.Distinct().Select(categoryId => new ProductCategory
        {
            ProductId = product.Id,
            CategoryId = categoryId
        }).ToList();

        await _context.SaveChangesAsync();

        return "Mehsul uqurla yenilendi.";
    }

    public async Task<string> DeleteAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            throw new KeyNotFoundException("Mehsul tapilmadi.");

        _context.ProductImages.RemoveRange(product.Images);
        _context.ProductCategories.RemoveRange(product.ProductCategories);
        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return "Mehsul  silindi.";
    }
}
