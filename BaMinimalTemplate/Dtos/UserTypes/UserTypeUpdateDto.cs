using System.ComponentModel.DataAnnotations;

namespace BaMinimalTemplate.Dtos;

public class UserTypeUpdateDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}