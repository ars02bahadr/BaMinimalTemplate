using System.ComponentModel.DataAnnotations;

namespace BaMinimalTemplate.Dtos.Users;

public class UserUpdateDto
{
    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string UserName { get; set; } = string.Empty;
    
    public Guid? UserTypeId { get; set; }
}