using BaMinimalTemplate.Dtos;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Services;

public interface IUserTypeService : IGenericService<UserType,UserTypeDto,UserTypeListDto,UserTypeCreateDto,UserTypeUpdateDto>
{
}