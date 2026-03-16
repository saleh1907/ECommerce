using ECommerce.Contexts;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Seed;

public static class DbInitializer
{
    public static async Task SeedAdminAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        var adminExists = await context.AppUsers.AnyAsync(x => x.Role == "Admin");

        if (!adminExists)
        {
            var admin = new AppUser
            {
                FullName = "System Admin",
                Email = "admin@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = "Admin"
            };

            await context.AppUsers.AddAsync(admin);
            await context.SaveChangesAsync();
        }
    }
}
