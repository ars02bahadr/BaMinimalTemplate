using BaMinimalTemplate.Data;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Repositories.Categories;

public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context) { }

}