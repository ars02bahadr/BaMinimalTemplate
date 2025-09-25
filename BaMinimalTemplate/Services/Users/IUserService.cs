using BaMinimalTemplate.Dtos.Users;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Services.Users;

public interface IUserService: IGenericService<User, UserDto, UserListDto, UserCreateDto, UserUpdateDto>
{
    
}