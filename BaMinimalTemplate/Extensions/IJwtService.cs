using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Extensions;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(User user);
    Task<string> GenerateRefreshTokenAsync(User user);
    Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken);
    Task<User?> GetUserFromTokenAsync(string token);
    string? GetUserIdFromToken(string token);
    bool IsTokenExpired(string token);
}