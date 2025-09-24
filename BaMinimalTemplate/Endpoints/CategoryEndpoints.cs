using BaMinimalTemplate.Dtos.Categories;
using BaMinimalTemplate.Services.Categories;

namespace BaMinimalTemplate.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var categories = app.MapGroup("/api/categories")
            .WithTags("Categories");


        // Admin endpoints
        var adminCategories = categories.MapGroup("")
            .RequireAuthorization();

        categories.MapGet("/all", async (ICategoryService categoryService, int page = 1, int pageSize = 10) =>
            {
                var result = await categoryService.GetPagedAsync(page, pageSize);
                return Results.Ok(result);
            })
            .WithSummary("Get all categories (admin)");

        categories.MapGet("/{id}", async (Guid id, ICategoryService categoryService) =>
            {
                var category = await categoryService.GetByIdAsync(id);
                return category != null ? Results.Ok(category) : Results.NotFound();
            })
            .WithSummary("Get category by ID");

        categories.MapPost("/", async (CategoryCreateDto dto, ICategoryService categoryService) =>
            {
                var category = await categoryService.CreateAsync(dto);
                return Results.Created($"/api/categoies/{category}", category);
            })
            .WithSummary("Create category");

        categories.MapPut("/{id}", async (Guid id, CategoryUpdateDto dto, ICategoryService categoryService) =>
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

        categories.MapDelete("/{id}", async (Guid id, ICategoryService categoryService) =>
            {
                var result = await categoryService.DeleteAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithSummary("Delete category");
    }
}
