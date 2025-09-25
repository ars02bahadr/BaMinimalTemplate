using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BaMinimalTemplate.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace BaMinimalTemplate.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var authGroup = endpoints.MapGroup("/api/auth")
            .WithTags("Authentication");

        // Login endpoint
        authGroup.MapPost("/login", async (
            [FromBody] LoginDto loginDto,
            IAuthService authService) =>
        {

            var result = await authService.LoginAsync(loginDto);
            if (result == null)
                return Results.Unauthorized();

            return Results.Ok(result);
        })
        .WithName("Login")
        .WithSummary("Kullanıcı girişi")
        .WithDescription("Email ve şifre ile kullanıcı girişi yapar")
        .Produces<TokenResponseDto>(200)
        .Produces(401)
        .Produces(400);

        // Register endpoint
        authGroup.MapPost("/register", async (
            [FromBody] RegisterDto registerDto,
            IAuthService authService) =>
        {

            var result = await authService.RegisterAsync(registerDto);
            if (result == null)
                return Results.BadRequest("Kullanıcı oluşturulamadı. Email zaten kullanımda olabilir.");

            return Results.Ok(result);
        })
        .WithName("Register")
        .WithSummary("Kullanıcı kaydı")
        .WithDescription("Yeni kullanıcı kaydı oluşturur")
        .Produces<TokenResponseDto>(200)
        .Produces(400);

        // Refresh token endpoint
        authGroup.MapPost("/refresh", async (
            [FromBody] RefreshTokenDto refreshTokenDto,
            IAuthService authService) =>
        {

            var result = await authService.RefreshTokenAsync(refreshTokenDto);
            if (result == null)
                return Results.Unauthorized();

            return Results.Ok(result);
        })
        .WithName("RefreshToken")
        .WithSummary("Token yenileme")
        .WithDescription("Refresh token ile yeni access token oluşturur")
        .Produces<TokenResponseDto>(200)
        .Produces(401)
        .Produces(400);

        // Logout endpoint
        authGroup.MapPost("/logout", [Authorize] async (
            ClaimsPrincipal user,
            IAuthService authService) =>
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();

            var result = await authService.LogoutAsync(userId);
            if (!result)
                return Results.BadRequest("Çıkış işlemi başarısız");

            return Results.Ok(new { message = "Başarıyla çıkış yapıldı" });
        })
        .WithName("Logout")
        .WithSummary("Kullanıcı çıkışı")
        .WithDescription("Kullanıcı oturumunu sonlandırır")
        .Produces(200)
        .Produces(401)
        .Produces(400)
        .RequireAuthorization();

        // Change password endpoint
        authGroup.MapPost("/change-password", [Authorize] async (
            [FromBody] ChangePasswordDto changePasswordDto,
            ClaimsPrincipal user,
            IAuthService authService) =>
        {

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();

            var result = await authService.ChangePasswordAsync(
                userId, 
                changePasswordDto.CurrentPassword, 
                changePasswordDto.NewPassword);

            if (!result)
                return Results.BadRequest("Şifre değiştirme işlemi başarısız. Mevcut şifrenizi kontrol edin.");

            return Results.Ok(new { message = "Şifre başarıyla değiştirildi" });
        })
        .WithName("ChangePassword")
        .WithSummary("Şifre değiştirme")
        .WithDescription("Kullanıcı şifresini değiştirir")
        .Produces(200)
        .Produces(401)
        .Produces(400)
        .RequireAuthorization();

        // Forgot password endpoint
        authGroup.MapPost("/forgot-password", async (
            [FromBody] ForgotPasswordDto forgotPasswordDto,
            IAuthService authService) =>
        {

            var result = await authService.ForgotPasswordAsync(forgotPasswordDto.Email);
            
            // Always return success to prevent email enumeration
            return Results.Ok(new { message = "Eğer email sistemde kayıtlıysa, şifre sıfırlama bağlantısı gönderildi" });
        })
        .WithName("ForgotPassword")
        .WithSummary("Şifremi unuttum")
        .WithDescription("Şifre sıfırlama bağlantısı gönderir")
        .Produces(200)
        .Produces(400);

        // Get current user info
        authGroup.MapGet("/me", [Authorize] async (
            ClaimsPrincipal user) =>
        {
            var userInfo = new
            {
                Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Email = user.FindFirst(ClaimTypes.Email)?.Value,
                FirstName = user.FindFirst(ClaimTypes.GivenName)?.Value,
                LastName = user.FindFirst(ClaimTypes.Surname)?.Value,
                UserTypeId = user.FindFirst("user_type_id")?.Value,
                UserType = user.FindFirst("user_type")?.Value,
                Roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
            };

            return Results.Ok(userInfo);
        })
        .WithName("GetCurrentUser")
        .WithSummary("Mevcut kullanıcı bilgileri")
        .WithDescription("Giriş yapmış kullanıcının bilgilerini getirir")
        .Produces(200)
        .Produces(401)
        .RequireAuthorization();
    }
}


