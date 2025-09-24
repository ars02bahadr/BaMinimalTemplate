using BaMinimalTemplate.Extensions;

namespace BaMinimalTemplate.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty; // ProjectManager, Developer, Designer, etc.
    public string Description { get; set; } = string.Empty;

    // Many-to-Many Relationship with Users
    public ICollection<UserCategory> UserCategories { get; set; } = new List<UserCategory>();
}