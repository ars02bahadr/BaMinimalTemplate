using BaMinimalTemplate.Extensions;

namespace BaMinimalTemplate.Models;

public class UserType : BaseEntity
{
    public string Name { get; set; } = string.Empty; // Admin, Manager, Employee, Customer
    public string Description { get; set; } = string.Empty;

    // One-to-Many Relationship with Users
    public ICollection<User> Users { get; set; } = new List<User>();
}