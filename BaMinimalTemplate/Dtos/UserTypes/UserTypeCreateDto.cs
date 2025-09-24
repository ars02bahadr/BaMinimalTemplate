using System.ComponentModel.DataAnnotations;

namespace BaMinimalTemplate.Dtos;

public class UserTypeCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}