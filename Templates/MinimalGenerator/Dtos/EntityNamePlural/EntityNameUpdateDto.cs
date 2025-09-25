using System.ComponentModel.DataAnnotations;
namespace __Namespace__.Dtos.EntityNamePlural;
public class EntityNameUpdateDto
{
    [Required] public Guid Id { get; set; }
    
    // TODO: Entity özelliklerini buraya ekleyin (CreatedAt, UpdatedAt, IsDeleted hariç)
    // Örnek:
    // [Required] public string Name { get; set; } = string.Empty;
    // public string? Description { get; set; }
    // [Range(0, double.MaxValue)] public decimal Price { get; set; }
    // public bool IsActive { get; set; }
}
