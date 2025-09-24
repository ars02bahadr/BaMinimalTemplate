using AutoMapper;
using BaMinimalTemplate.Dtos.Categories;

namespace BaMinimalTemplate.Mapping.UserTypes;

public class CategoryMapper:Profile
{
    public CategoryMapper()
    {
        // Category mappings
        CreateMap<Models.Category, CategoryDto>();
        CreateMap<Models.Category, CategoryListDto>();
        CreateMap<CategoryCreateDto, Models.Category>();
        CreateMap<CategoryUpdateDto, Models.Category>();
        // Add other entity mappings here
    }
}