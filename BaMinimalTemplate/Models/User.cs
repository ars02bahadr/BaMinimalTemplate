using Microsoft.AspNetCore.Identity;

namespace BaMinimalTemplate.Models;

public class User:IdentityUser<Guid>
{
    public string FirstName { get; set; }=string.Empty;
    public string LastName { get; set; }=string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }
    public ICollection<UserCategory> UserCategories { get; set; } = new List<UserCategory>();
    public UserProfile? Profile { get; set; }

    // Many-to-One Relationship with UserType
    public Guid? UserTypeId { get; set; }
    public UserType? UserType { get; set; }
}