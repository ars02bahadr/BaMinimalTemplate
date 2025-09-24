using System.Linq.Expressions;
using AutoMapper;
using BaMinimalTemplate.Dtos;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;
using BaMinimalTemplate.Repositories;

namespace BaMinimalTemplate.Services;

public class UserTypeService : GenericService<UserType, UserTypeDto,UserTypeListDto,UserTypeCreateDto,UserTypeUpdateDto>, IUserTypeService
{
    private readonly IUserTypeRepository _userTypeRepository;

    public UserTypeService(IUserTypeRepository userTypeRepository, IMapper mapper) 
        : base(userTypeRepository, mapper) 
    {
        _userTypeRepository = userTypeRepository;
    }

    
}