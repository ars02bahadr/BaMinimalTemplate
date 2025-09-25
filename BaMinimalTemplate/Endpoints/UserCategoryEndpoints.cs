using BaMinimalTemplate.Dtos.UserCategories;
using BaMinimalTemplate.Services.UserCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BaMinimalTemplate.Endpoints;

public class UserCategoryEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var userCategories = app.MapGroup("/api/user-categories")
            .WithTags("User Categories");

        // Admin endpoints - require authorization
        var adminUserCategories = userCategories.MapGroup("")
            .RequireAuthorization();

        // Get all user categories
        adminUserCategories.MapGet("/all", async (IUserCategoryService userCategoryService, int page = 1, int pageSize = 10) =>
            {
                var result = await userCategoryService.GetPagedAsync(page, pageSize);
                return Results.Ok(result);
            })
            .WithSummary("Get all user categories")
            .WithDescription("Tüm kullanıcı-kategori atamalarını getirir");

        // Get user category by ID
        adminUserCategories.MapGet("/{id}", async (Guid id, IUserCategoryService userCategoryService) =>
            {
                var userCategory = await userCategoryService.GetByIdAsync(id);
                return userCategory != null ? Results.Ok(userCategory) : Results.NotFound();
            })
            .WithSummary("Get user category by ID")
            .WithDescription("ID'ye göre kullanıcı-kategori atamasını getirir");

        // Create new user category assignment
        adminUserCategories.MapPost("/", async ([FromBody] UserCategoryCreateDto dto, IUserCategoryService userCategoryService) =>
            {
                try
                {
                    var userCategory = await userCategoryService.CreateAsync(dto);
                    return Results.Created($"/api/user-categories/{userCategory.Id}", userCategory);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            })
            .WithSummary("Create user category assignment")
            .WithDescription("Yeni kullanıcı-kategori ataması oluşturur");

        // Update user category assignment
        adminUserCategories.MapPut("/{id}", async (Guid id, [FromBody] UserCategoryUpdateDto dto, IUserCategoryService userCategoryService) =>
            {
                try
                {
                    var userCategory = await userCategoryService.UpdateAsync(id, dto);
                    return Results.Ok(userCategory);
                }
                catch (ArgumentException)
                {
                    return Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            })
            .WithSummary("Update user category assignment")
            .WithDescription("Kullanıcı-kategori atamasını günceller");

        // Delete user category assignment
        adminUserCategories.MapDelete("/{id}", async (Guid id, IUserCategoryService userCategoryService) =>
            {
                var result = await userCategoryService.DeleteAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithSummary("Delete user category assignment")
            .WithDescription("Kullanıcı-kategori atamasını siler");

        // Get categories by user ID
        adminUserCategories.MapGet("/user/{userId}", async (Guid userId, IUserCategoryService userCategoryService) =>
            {
                var userCategories = await userCategoryService.GetByUserIdAsync(userId);
                return Results.Ok(userCategories);
            })
            .WithSummary("Get categories by user ID")
            .WithDescription("Belirli bir kullanıcının kategorilerini getirir");

        // Get users by category ID
        adminUserCategories.MapGet("/category/{categoryId}", async (Guid categoryId, IUserCategoryService userCategoryService) =>
            {
                var userCategories = await userCategoryService.GetByCategoryIdAsync(categoryId);
                return Results.Ok(userCategories);
            })
            .WithSummary("Get users by category ID")
            .WithDescription("Belirli bir kategoriye atanmış kullanıcıları getirir");

        // Assign category to user (convenience endpoint)
        adminUserCategories.MapPost("/assign", async ([FromBody] UserCategoryCreateDto dto, IUserCategoryService userCategoryService) =>
            {
                try
                {
                    var userCategory = await userCategoryService.AssignCategoryToUserAsync(dto.UserId, dto.CategoryId);
                    return Results.Created($"/api/user-categories/{userCategory.Id}", userCategory);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            })
            .WithSummary("Assign category to user")
            .WithDescription("Kategoriyi kullanıcıya atar");

        // Remove category from user (convenience endpoint)
        adminUserCategories.MapDelete("/user/{userId}/category/{categoryId}", async (Guid userId, Guid categoryId, IUserCategoryService userCategoryService) =>
            {
                var result = await userCategoryService.RemoveCategoryFromUserAsync(userId, categoryId);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithSummary("Remove category from user")
            .WithDescription("Kullanıcıdan kategoriyi kaldırır");

        // Get current user's categories
        adminUserCategories.MapGet("/my-categories", [Authorize] async (ClaimsPrincipal user, IUserCategoryService userCategoryService) =>
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                {
                    return Results.Unauthorized();
                }

                var userCategories = await userCategoryService.GetByUserIdAsync(userId);
                return Results.Ok(userCategories);
            })
            .WithSummary("Get current user's categories")
            .WithDescription("Giriş yapmış kullanıcının kategorilerini getirir");
    }
}