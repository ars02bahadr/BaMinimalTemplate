namespace __Namespace__.Services.EntityNamePlural;

public class EntityNameService
  : GenericService<EntityName, EntityNameDto, EntityNameListDto, EntityNameCreateDto, EntityNameUpdateDto>,
    IEntityNameService
{
    private readonly IEntityNameRepository _repo;
    public EntityNameService(IEntityNameRepository repo, IMapper mapper) : base(repo, mapper)
        => _repo = repo;
}
