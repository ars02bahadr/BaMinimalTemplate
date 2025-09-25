using __Namespace__.Data;
using __Namespace__.Extensions;
using __Namespace__.Models;

namespace __Namespace__.Repositories.EntityNamePlural;

public class EntityNameRepository : GenericRepository<EntityName>, IEntityNameRepository
{
    public EntityNameRepository(ApplicationDbContext context) : base(context) { }
}
