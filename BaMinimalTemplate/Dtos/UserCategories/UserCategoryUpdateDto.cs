using System.ComponentModel.DataAnnotations;

namespace BaMinimalTemplate.Dtos.UserCategories;

public class UserCategoryUpdateDto
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid CategoryId { get; set; }
}
