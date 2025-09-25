using BaMinimalTemplate.Dtos.Categories;
using BaMinimalTemplate.Dtos.Users;

namespace BaMinimalTemplate.Dtos.UserCategories;

public class UserCategoryListDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
