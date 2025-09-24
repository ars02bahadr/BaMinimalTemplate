using BaMinimalTemplate.Extensions;

namespace BaMinimalTemplate.Models;

public class UserProfile:BaseEntity
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public string Address { get; set; }=string.Empty;
    public string ProfilePictureUrl { get; set; }=string.Empty;
}