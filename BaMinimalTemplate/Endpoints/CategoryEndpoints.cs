using BaMinimalTemplate.Dtos.Categories;
using BaMinimalTemplate.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BaMinimalTemplate.Endpoints;

public class CategoryEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var categories = app.MapGroup("/api/categories")
            .WithTags("Categories");


        // Admin endpoints
        var adminCategories = categories.MapGroup("")
            .RequireAuthorization();

        adminCategories.MapGet("/all", async (ICategoryService categoryService, int page = 1, int pageSize = 10) =>
            {
                var result = await categoryService.GetPagedAsync(page, pageSize);
                return Results.Ok(result);
            })
            .WithSummary("Get all categories (admin)");

        adminCategories.MapGet("/{id}", async (Guid id, ICategoryService categoryService) =>
            {
                var category = await categoryService.GetByIdAsync(id);
                return category != null ? Results.Ok(category) : Results.NotFound();
            })
            .WithSummary("Get category by ID");

        adminCategories.MapPost("/", async (CategoryCreateDto dto, ICategoryService categoryService) =>
            {
                var category = await categoryService.CreateAsync(dto);
                return Results.Created($"/api/categoies/{category}", category);
            })
            .WithSummary("Create category");

        adminCategories.MapPut("/{id}", async (Guid id, CategoryUpdateDto dto, ICategoryService categoryService) =>
            {
                try
                {
                    var category = await categoryService.UpdateAsync(id, dto);
                    return Results.Ok(category);
                }
                catch (ArgumentException)
                {
                    return Results.NotFound();
                }
            })
            .WithSummary("Update category");

        adminCategories.MapDelete("/{id}", async (Guid id, ICategoryService categoryService) =>
            {
                var result = await categoryService.DeleteAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithSummary("Delete category");
    }
}
