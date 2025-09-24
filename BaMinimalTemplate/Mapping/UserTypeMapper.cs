using AutoMapper;
using BaMinimalTemplate.Dtos;
using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Mapping.UserTypes;

public class UserTypeMapper: Profile
{
    public UserTypeMapper()
    {
            
        // UserType mappings
        CreateMap<UserType, UserTypeListDto>();
        CreateMap<UserType, UserTypeDto>();
        CreateMap<UserTypeCreateDto, UserType>();
        CreateMap<UserTypeUpdateDto, UserType>();
        // Add other entity mappings here
        
       
    }
}