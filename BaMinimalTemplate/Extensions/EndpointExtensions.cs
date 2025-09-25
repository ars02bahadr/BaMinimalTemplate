using System.Reflection;
using BaMinimalTemplate.Endpoints;

namespace BaMinimalTemplate.Extensions;

public static class EndpointExtensions
{
    public static IApplicationBuilder MapDiscoveredEndpoints(this IApplicationBuilder app)
    {
        var builder = (IEndpointRouteBuilder)app;
        var endpointTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract && typeof(IEndpoint).IsAssignableFrom(t));

        foreach (var t in endpointTypes)
        {
            var ep = (IEndpoint)Activator.CreateInstance(t)!;
            ep.MapEndpoints(builder);
        }
        return app;
    }
}
