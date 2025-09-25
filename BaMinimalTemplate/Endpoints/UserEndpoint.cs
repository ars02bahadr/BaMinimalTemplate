using System.Security.Claims;
using BaMinimalTemplate.Dtos.Users;
using BaMinimalTemplate.Services.Users;

namespace BaMinimalTemplate.Endpoints;

public class UserEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var users = app.MapGroup("/api/users").WithTags("Users");

        var adminUsers = users.MapGroup("")
            .RequireAuthorization();

        adminUsers.MapGet("/all", async (IUserService userService, int page = 1, int pageSize = 10) =>
        {
            var result = await userService.GetPagedAsync(page, pageSize);
            return Results.Ok(result);
        })
        .WithSummary("Get all users")
        .WithDescription("Tüm kullanıcıları listeler. Admin yetkisi gerektirir.")
        .RequireAuthorization("AdminOnly");

        adminUsers.MapGet("/{id}", async (Guid id, IUserService userService, ClaimsPrincipal currentUser) =>
        {
            // Kullanıcının kendi bilgilerini görebileceğini veya admin yetkisi olduğunu kontrol et
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId) || !Guid.TryParse(currentUserId, out var currentUserGuid))
            {
                return Results.Unauthorized();
            }

            // Kullanıcı kendi bilgilerini görmek istiyorsa veya admin yetkisi varsa izin ver
            bool canView = currentUserGuid == id || currentUser.HasClaim("permissions", "admin:full");

            if (!canView)
            {
                return Results.Forbid();
            }

            var user = await userService.GetByIdAsync(id);
            return user != null ? Results.Ok(user) : Results.NotFound();
        })
        .WithSummary("Get user")
        .WithDescription("Kendi bilgilerinizi görebilir veya admin yetkisi gerektirir.")
        .RequireAuthorization();

        adminUsers.MapPost("/", async (UserCreateDto dto, IUserService userService) =>
        {
            var user = await userService.CreateAsync(dto);
            return Results.Created($"/api/users/{user.Id}", user);
        })
        .WithSummary("Create user")
        .WithDescription("Yeni kullanıcı oluşturur. Admin yetkisi gerektirir.")
        .RequireAuthorization("AdminOnly");

        adminUsers.MapPut("/{id}", async (Guid id, UserUpdateDto dto, IUserService userService, ClaimsPrincipal currentUser) =>
        {
            // Kullanıcının kendisini güncelleyip güncelleyemeyeceğini kontrol et
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId) || !Guid.TryParse(currentUserId, out var currentUserGuid))
            {
                return Results.Unauthorized();
            }

            // Kullanıcı kendisini güncelliyorsa veya admin yetkisi varsa izin ver
            bool canUpdate = currentUserGuid == id || currentUser.HasClaim("permissions", "user:update");

            if (!canUpdate)
            {
                return Results.Forbid();
            }

            try
            {
                var user = await userService.UpdateAsync(id, dto);
                return Results.Ok(user);
            }
            catch (ArgumentException)
            {
                return Results.NotFound();
            }
        })
        .WithSummary("Update user")
        .WithDescription("Kendi hesabınızı güncelleyebilir veya user:update yetkisi gerektirir.")
        .RequireAuthorization();

        adminUsers.MapDelete("/{id}", async (Guid id, IUserService userService, ClaimsPrincipal user) =>
        {
            // Kullanıcının kendisini silip silemeyeceğini kontrol et
            var currentUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId) || !Guid.TryParse(currentUserId, out var currentUserGuid))
            {
                return Results.Unauthorized();
            }

            // Kullanıcı kendisini silmeye çalışıyorsa
            if (currentUserGuid == id)
            {
                return Results.BadRequest(new { message = "Kendi hesabınızı silemezsiniz." });
            }

            var result = await userService.DeleteAsync(id);
            return result ? Results.NoContent() : Results.NotFound();
        })
        .WithSummary("Delete user")
        .WithDescription("Kullanıcı silme yetkisi gerektirir. Kendi hesabınızı silemezsiniz.")
        .RequireAuthorization("CanDeleteUsers");
    }
}