namespace __Namespace__.Endpoints;

public class EntityNameEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/EntityNamePlural").WithTags("EntityName");
        
        group.MapGet("/", async (IEntityNameService svc, int page = 1, int pageSize = 20, string? q = null)
            => Results.Ok(await svc.GetPagedAsync(page, pageSize, q)));

        group.MapGet("/{id:guid}", async (IEntityNameService svc, Guid id)
            => Results.Ok(await svc.GetByIdAsync(id)));

        group.MapPost("/", async (IEntityNameService svc, EntityNameCreateDto dto)
            => Results.Ok(await svc.CreateAsync(dto)));

        group.MapPut("/{id:guid}", async (IEntityNameService svc,Guid id, EntityNameUpdateDto dto)
            => Results.Ok(await svc.UpdateAsync(id, dto)));

        group.MapDelete("/{id:guid}", async (IEntityNameService svc, Guid id)
            => Results.Ok(await svc.DeleteAsync(id)));
    }
}
