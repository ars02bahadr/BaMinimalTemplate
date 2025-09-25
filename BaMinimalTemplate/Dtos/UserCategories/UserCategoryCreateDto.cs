using System.ComponentModel.DataAnnotations;

namespace BaMinimalTemplate.Dtos.UserCategories;

public class UserCategoryCreateDto
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid CategoryId { get; set; }
}
