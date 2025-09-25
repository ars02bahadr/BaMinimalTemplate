using __Namespace__.Extensions;
using __Namespace__.Models;
using __Namespace__.Dtos.EntityNamePlural;

namespace __Namespace__.Services.EntityNamePlural;

public interface IEntityNameService
    : IGenericService<EntityName, EntityNameDto, EntityNameListDto, EntityNameCreateDto, EntityNameUpdateDto> { }
