using System.Linq.Expressions;
using BaMinimalTemplate.Data;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace BaMinimalTemplate.Repositories.UserCategories;

public class UserCategoryRepository : GenericRepository<UserCategory>, IUserCategoryRepository
{
    private readonly ApplicationDbContext _context;

    public UserCategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    

    public async Task<IEnumerable<UserCategory>> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserCategories
            .Include(uc => uc.User)
            .Include(uc => uc.Category)
            .Where(uc => uc.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserCategory>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await _context.UserCategories
            .Include(uc => uc.User)
            .Include(uc => uc.Category)
            .Where(uc => uc.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<UserCategory?> GetByUserAndCategoryAsync(Guid userId, Guid categoryId)
    {
        return await _context.UserCategories
            .Include(uc => uc.User)
            .Include(uc => uc.Category)
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CategoryId == categoryId);
    }

    public async Task<bool> ExistsAsync(Guid userId, Guid categoryId)
    {
        return await _context.UserCategories
            .AnyAsync(uc => uc.UserId == userId && uc.CategoryId == categoryId);
    }

    public async Task<(IEnumerable<UserCategory> Items, int TotalCount)> GetPagedAsync<TKey>(int page, int pageSize, Expression<Func<UserCategory, bool>>? predicate = null, Expression<Func<UserCategory, TKey>>? orderBy = null,
        bool orderByDescending = false)
    {
        var baseQuery = _context.UserCategories
            .Include(x => x.User)
            .Include(x => x.Category);
        
        var filteredQuery = predicate != null ? baseQuery.Where(predicate) : baseQuery;

        var totalCount = await filteredQuery.CountAsync();

        IQueryable<UserCategory> orderedQuery;
        if (orderBy != null)
        {
            orderedQuery = orderByDescending ? filteredQuery.OrderByDescending(orderBy) : filteredQuery.OrderBy(orderBy);
        }
        else
        {
            // Default ordering by CreatedAt
            orderedQuery = filteredQuery.OrderByDescending(x => x.CreatedAt);
        }

        var items = await orderedQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}
