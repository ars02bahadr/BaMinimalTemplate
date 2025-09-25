using AutoMapper;
using BaMinimalTemplate.Dtos.Users;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaMinimalTemplate.Services.Users;

public class UserService:GenericService<User, UserDto, UserListDto, UserCreateDto, UserUpdateDto>, IUserService
{
    private readonly UserManager<User> _userManager;
    public UserService(UserManager<User> userManager, IGenericRepository<User> genericRepository, IMapper mapper) 
        : base(genericRepository, mapper)
    {
        _userManager = userManager;
    }

    public async Task<PagedResultDto<UserListDto>> GetPagedAsync(int page, int pageSize, string? search = null)
    {
        var query = _userManager.Users.Include(x => x.UserType).AsQueryable();
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(u => u.FirstName.Contains(search) || 
                                     u.LastName.Contains(search) || 
                                     u.Email.Contains(search));
        }
    
        var totalCount = await query.CountAsync();
    
        var users = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    
        var userDtos = _mapper.Map<IEnumerable<UserListDto>>(users);
    
        return new PagedResultDto<UserListDto>
        {
            Items = userDtos,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        };
    }
    
    public override async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _userManager.Users.Include(x=>x.UserType).FirstOrDefaultAsync(x=>x.Id==id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public override async Task<UserDto> CreateAsync(UserCreateDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        return _mapper.Map<UserDto>(user);
    }

    public override async Task<UserDto> UpdateAsync(Guid id, UserUpdateDto userDto)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }
        _mapper.Map(userDto, user);
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        return _mapper.Map<UserDto>(user);
    }
    
    public override async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return false;
        }
        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }
}