using BaMinimalTemplate.Dtos;
using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Services.Auth;

public interface IAuthService
{
    Task<TokenResponseDto?> LoginAsync(LoginDto loginDto);
    Task<TokenResponseDto?> RegisterAsync(RegisterDto registerDto);
    Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
    Task<bool> LogoutAsync(string userId);
    Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    Task<bool> ForgotPasswordAsync(string email);
    Task<bool> ResetPasswordAsync(string userId, string token, string newPassword);
}
