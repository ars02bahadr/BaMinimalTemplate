using AutoMapper;
using BaMinimalTemplate.Dtos.Categories;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;
using BaMinimalTemplate.Repositories.Categories;

namespace BaMinimalTemplate.Services.Categories;

public class CategoryService: GenericService<Category, CategoryDto,CategoryListDto,CategoryCreateDto,CategoryUpdateDto>, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) 
        : base(categoryRepository, mapper) 
    {
        _categoryRepository = categoryRepository;
    }

    
}