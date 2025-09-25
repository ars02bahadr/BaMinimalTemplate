using System.ComponentModel.DataAnnotations;

namespace __Namespace__.Dtos.EntityNamePlural;

public class EntityNameDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    
    // TODO: Entity özelliklerini buraya ekleyin
    // Model sınıfından property'leri kopyalayın
    // Örnek:
    // public string Name { get; set; } = string.Empty;
    // public string Description { get; set; } = string.Empty;
    // public decimal Price { get; set; }
    // public bool IsActive { get; set; } = true;
}
