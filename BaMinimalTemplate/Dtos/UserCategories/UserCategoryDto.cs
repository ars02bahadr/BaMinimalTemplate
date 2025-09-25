using BaMinimalTemplate.Dtos.Categories;
using BaMinimalTemplate.Dtos.Users;

namespace BaMinimalTemplate.Dtos.UserCategories;

public class UserCategoryDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserDto User { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public CategoryDto Category { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
