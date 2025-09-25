using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace BaMinimalTemplate.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AutoMapper: assembly taraması
        services.AddSingleton(_ => new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        }).CreateMapper());

        // Generic altyapın
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericService<,,,,>), typeof(GenericService<,,,,>));

        // Sonek'e göre otomatik DI (Repository, Service)
        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(c => c.Where(t => t.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(c => c.Where(t => t.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        ServiceRegistrationPartial(services);
        return services;
    }
    
    static partial void ServiceRegistrationPartial(IServiceCollection services);
}