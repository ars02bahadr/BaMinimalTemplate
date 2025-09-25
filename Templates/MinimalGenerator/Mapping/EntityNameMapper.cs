using AutoMapper;
using __Namespace__.Dtos.EntityNamePlural;

namespace __Namespace__.Mapping;

public class EntityNameMapper : Profile
{
    public EntityNameMapper()
    {
        CreateMap<Models.EntityName, EntityNameDto>();
        CreateMap<Models.EntityName, EntityNameListDto>();
        CreateMap<EntityNameCreateDto, Models.EntityName>();
        CreateMap<EntityNameUpdateDto, Models.EntityName>();
    }
}
