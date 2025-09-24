using BaMinimalTemplate.Dtos;

namespace BaMinimalTemplate.Extensions;

public interface IGenericService<TEntity,TDto,TListDto,TCreateDto,TUpdateDto> where TEntity : class where TDto : class where TListDto : class where TCreateDto : class where TUpdateDto : class
{
    Task<TDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<PagedResultDto<TListDto>> GetPagedAsync(int page, int pageSize, string? search = null);
    Task<TDto> CreateAsync(TCreateDto createDto);
    Task<TDto> UpdateAsync(Guid id,TUpdateDto updateTUpdateDtoDto);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<int> CountAsync();
}