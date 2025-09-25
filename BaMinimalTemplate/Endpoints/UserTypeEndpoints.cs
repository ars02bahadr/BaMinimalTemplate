using BaMinimalTemplate.Dtos;
using BaMinimalTemplate.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BaMinimalTemplate.Endpoints;

public class UserTypeEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var userTypes = app.MapGroup("/api/user-types")
            .WithTags("User Types");

        // Admin endpoints
        var adminUserTypes = userTypes.MapGroup("")
            .RequireAuthorization();

        userTypes.MapGet("/all", async (IUserTypeService userTypeService, int page = 1, int pageSize = 10) =>
        {
            var result = await userTypeService.GetPagedAsync(page, pageSize);
            return Results.Ok(result);
        })
        .WithSummary("Get all user types (admin)");

        userTypes.MapGet("/{id}", async (Guid id, IUserTypeService userTypeService) =>
        {
            var userType = await userTypeService.GetByIdAsync(id);
            return userType != null ? Results.Ok(userType) : Results.NotFound();
        })
        .WithSummary("Get user type by ID");

        userTypes.MapPost("/", async (UserTypeCreateDto dto, IUserTypeService userTypeService) =>
        {
            var userType = await userTypeService.CreateAsync(dto);
            return Results.Created($"/api/user-types/{userType}", userType);
        })
        .WithSummary("Create user type");

        userTypes.MapPut("/{id}", async (Guid id, UserTypeUpdateDto dto, IUserTypeService userTypeService) =>
        {
            try
            {
                var userType = await userTypeService.UpdateAsync(id, dto);
                return Results.Ok(userType);
            }
            catch (ArgumentException)
            {
                return Results.NotFound();
            }
        })
        .WithSummary("Update user type");

        userTypes.MapDelete("/{id}", async (Guid id, IUserTypeService userTypeService) =>
        {
            var result = await userTypeService.DeleteAsync(id);
            return result ? Results.NoContent() : Results.NotFound();
        })
        .WithSummary("Delete user type");
    }
}