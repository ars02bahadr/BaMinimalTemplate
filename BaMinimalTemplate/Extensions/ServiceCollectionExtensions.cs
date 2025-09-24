using System.Reflection;
using BaMinimalTemplate.Models;
using BaMinimalTemplate.Repositories;
using BaMinimalTemplate.Repositories.Categories;
using BaMinimalTemplate.Services;
using BaMinimalTemplate.Services.Categories;

namespace BaMinimalTemplate.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        
        // Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserTypeRepository, UserTypeRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        services.AddScoped(typeof(IGenericService<,,,,>), typeof(GenericService<,,,,>));
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserTypeService, UserTypeService>();
        services.AddScoped<ICategoryService, CategoryService>();
        
        return services;
    }
}