using System.ComponentModel.DataAnnotations;

namespace BaMinimalTemplate.Dtos.Users;

public class UserCreateDto
{
    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public string UserName { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
    
    [Required, Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;
    
    public Guid? UserTypeId { get; set; }
}