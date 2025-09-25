using AutoMapper;
using BaMinimalTemplate.Dtos.UserCategories;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;
using BaMinimalTemplate.Repositories.UserCategories;
using Microsoft.EntityFrameworkCore;

namespace BaMinimalTemplate.Services.UserCategories;

public class UserCategoryService : GenericService<UserCategory, UserCategoryDto, UserCategoryListDto, UserCategoryCreateDto, UserCategoryUpdateDto>, IUserCategoryService
{
    private readonly IUserCategoryRepository _userCategoryRepository;

    public UserCategoryService(IUserCategoryRepository userCategoryRepository, IMapper mapper) 
        : base(userCategoryRepository, mapper)
    {
        _userCategoryRepository = userCategoryRepository;
    }

    public async Task<IEnumerable<UserCategoryListDto>> GetByUserIdAsync(Guid userId)
    {
        var userCategories = await _userCategoryRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<UserCategoryListDto>>(userCategories);
    }

    public async Task<IEnumerable<UserCategoryListDto>> GetByCategoryIdAsync(Guid categoryId)
    {
        var userCategories = await _userCategoryRepository.GetByCategoryIdAsync(categoryId);
        return _mapper.Map<IEnumerable<UserCategoryListDto>>(userCategories);
    }

    public async Task<UserCategoryDto?> GetByUserAndCategoryAsync(Guid userId, Guid categoryId)
    {
        var userCategory = await _userCategoryRepository.GetByUserAndCategoryAsync(userId, categoryId);
        return userCategory == null ? null : _mapper.Map<UserCategoryDto>(userCategory);
    }

    public async Task<bool> ExistsAsync(Guid userId, Guid categoryId)
    {
        return await _userCategoryRepository.ExistsAsync(userId, categoryId);
    }

    public async Task<UserCategoryDto> AssignCategoryToUserAsync(Guid userId, Guid categoryId)
    {
        // Check if assignment already exists
        if (await ExistsAsync(userId, categoryId))
        {
            throw new InvalidOperationException("Bu kategori zaten kullanıcıya atanmış.");
        }

        var userCategory = new UserCategory
        {
            UserId = userId,
            CategoryId = categoryId
        };

        var result = await _userCategoryRepository.AddAsync(userCategory);

        return _mapper.Map<UserCategoryDto>(result);
    }

    public async Task<bool> RemoveCategoryFromUserAsync(Guid userId, Guid categoryId)
    {
        var userCategory = await _userCategoryRepository.GetByUserAndCategoryAsync(userId, categoryId);
        if (userCategory == null)
        {
            return false;
        }

        await _userCategoryRepository.DeleteAsync(userCategory);
        return true;
    }

    public override async Task<UserCategoryDto> CreateAsync(UserCategoryCreateDto createDto)
    {
        // Check if assignment already exists
        if (await ExistsAsync(createDto.UserId, createDto.CategoryId))
        {
            throw new InvalidOperationException("Bu kategori zaten kullanıcıya atanmış.");
        }

        var userCategory = _mapper.Map<UserCategory>(createDto);
        var result = await _userCategoryRepository.AddAsync(userCategory);

        return _mapper.Map<UserCategoryDto>(result);
    }

    public override async Task<UserCategoryDto> UpdateAsync(Guid id, UserCategoryUpdateDto updateDto)
    {
        var existingUserCategory = await _userCategoryRepository.GetByIdAsync(id);
        if (existingUserCategory == null)
        {
            throw new ArgumentException("UserCategory not found");
        }

        // Check if new assignment already exists (excluding current one)
        if (await ExistsAsync(updateDto.UserId, updateDto.CategoryId) && 
            !(existingUserCategory.UserId == updateDto.UserId && existingUserCategory.CategoryId == updateDto.CategoryId))
        {
            throw new InvalidOperationException("Bu kategori zaten kullanıcıya atanmış.");
        }

        _mapper.Map(updateDto, existingUserCategory);
        var result = _userCategoryRepository.UpdateAsync(existingUserCategory);

        return _mapper.Map<UserCategoryDto>(result);
    }
}
