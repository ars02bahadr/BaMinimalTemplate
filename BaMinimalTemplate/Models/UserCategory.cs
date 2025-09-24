using BaMinimalTemplate.Extensions;

namespace BaMinimalTemplate.Models;

public class UserCategory:BaseEntity
{
    public Guid UserId { get; set; } 
    public User User { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    
}