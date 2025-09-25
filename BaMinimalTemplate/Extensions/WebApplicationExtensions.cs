using BaMinimalTemplate.Data;
using BaMinimalTemplate.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BaMinimalTemplate.Extensions;

public static class WebApplicationExtensions
{
    public static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();
        
        // Seed default admin user

        // Eğer daha önce rol oluşturuyorsan, o kodları SİL:
        // var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();  <-- KALDKIR
        // await roleManager.CreateAsync(new IdentityRole("Admin"));              <-- KALDKIR

        // => Kullanıcı seed'i gerekiyorsa yap:
        var adminEmail = "admin@example.com";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new User { UserName = "admin", Email = adminEmail };
            await userManager.CreateAsync(admin, "Passw0rd!");
            // ROL ATA YOK, çünkü roller yok
        }
        
        return app;
    }
    
    private static async Task SeedDefaultUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Create default roles
        var roles = new[] { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        
        // Create default admin user
        const string adminEmail = "admin@example.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "System",
                LastName = "Admin",
                EmailConfirmed = true,
                UserTypeId = Guid.NewGuid(), // Admin UserType
            };
            
            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}