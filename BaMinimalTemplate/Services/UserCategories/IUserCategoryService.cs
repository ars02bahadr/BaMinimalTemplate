using BaMinimalTemplate.Dtos.UserCategories;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Services.UserCategories;

public interface IUserCategoryService : IGenericService<UserCategory, UserCategoryDto, UserCategoryListDto, UserCategoryCreateDto, UserCategoryUpdateDto>
{
    Task<IEnumerable<UserCategoryListDto>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<UserCategoryListDto>> GetByCategoryIdAsync(Guid categoryId);
    Task<UserCategoryDto?> GetByUserAndCategoryAsync(Guid userId, Guid categoryId);
    Task<bool> ExistsAsync(Guid userId, Guid categoryId);
    Task<UserCategoryDto> AssignCategoryToUserAsync(Guid userId, Guid categoryId);
    Task<bool> RemoveCategoryFromUserAsync(Guid userId, Guid categoryId);
}
