using BaMinimalTemplate.Dtos.Categories;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Services.Categories;

public interface ICategoryService: IGenericService<Category,CategoryDto,CategoryListDto,CategoryCreateDto,CategoryUpdateDto>
{
}