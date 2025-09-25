using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Repositories.UserCategories;

public interface IUserCategoryRepository : IGenericRepository<UserCategory>
{
    Task<IEnumerable<UserCategory>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<UserCategory>> GetByCategoryIdAsync(Guid categoryId);
    Task<UserCategory?> GetByUserAndCategoryAsync(Guid userId, Guid categoryId);
    Task<bool> ExistsAsync(Guid userId, Guid categoryId);
}
