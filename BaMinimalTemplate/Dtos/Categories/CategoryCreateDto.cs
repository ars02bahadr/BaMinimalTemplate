using System.ComponentModel.DataAnnotations;

namespace BaMinimalTemplate.Dtos.Categories;

public class CategoryCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}