using System.Reflection;
using BaMinimalTemplate.Models;
using BaMinimalTemplate.Repositories;
using BaMinimalTemplate.Repositories.Categories;
using BaMinimalTemplate.Repositories.UserCategories;
using BaMinimalTemplate.Services;
using BaMinimalTemplate.Services.Auth;
using BaMinimalTemplate.Services.Categories;
using BaMinimalTemplate.Services.UserCategories;
using BaMinimalTemplate.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace BaMinimalTemplate.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        
        // Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserTypeRepository, UserTypeRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserCategoryRepository, UserCategoryRepository>();
        
        services.AddScoped(typeof(IGenericService<,,,,>), typeof(GenericService<,,,,>));
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserTypeService, UserTypeService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserCategoryService, UserCategoryService>();
        
        return services;
    }
}