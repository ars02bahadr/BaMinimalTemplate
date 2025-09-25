using Microsoft.AspNetCore.Routing;

namespace BaMinimalTemplate.Endpoints;

public interface IEndpoint
{
    void MapEndpoints(IEndpointRouteBuilder app);
}
