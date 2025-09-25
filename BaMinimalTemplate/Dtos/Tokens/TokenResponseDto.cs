using BaMinimalTemplate.Dtos.Users;
using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Dtos;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public string TokenType { get; set; } = "Bearer";
    public UserDto User { get; set; } = null!;
}