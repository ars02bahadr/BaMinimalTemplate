namespace __Namespace__.Repositories.EntityNamePlural;

public class EntityNameRepository : GenericRepository<EntityName>, IEntityNameRepository
{
    public EntityNameRepository(ApplicationDbContext context) : base(context) { }
}
