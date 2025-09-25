using AutoMapper;
using BaMinimalTemplate.Dtos.Users;
using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Mapping.UserTypes;

public class UserMapper:Profile
{

    public UserMapper()
    {
        CreateMap<User,UserDto>();
        CreateMap<User,UserListDto>();
        CreateMap<UserCreateDto,User>();
        CreateMap<UserUpdateDto,User>();
        CreateMap<UserListDto, User>();
        CreateMap<UserUpdateDto, User>();
    }
}