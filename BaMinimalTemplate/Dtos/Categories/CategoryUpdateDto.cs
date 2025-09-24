using System.ComponentModel.DataAnnotations;

namespace BaMinimalTemplate.Dtos.Categories;

public class CategoryUpdateDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}