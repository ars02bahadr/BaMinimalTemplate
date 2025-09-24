using System.Linq.Expressions;
using AutoMapper;
using BaMinimalTemplate.Dtos;

namespace BaMinimalTemplate.Extensions;

public class GenericService<TEntity, TDto,TListDto,TCreateDto,TUpdateDto> : IGenericService<TEntity, TDto,TListDto,TCreateDto,TUpdateDto>
    where TEntity : class
    where TDto : class
    where TListDto : class
    where TCreateDto:class
    where TUpdateDto:class
{
    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<TDto?> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : _mapper.Map<TDto>(entity);
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public virtual async Task<PagedResultDto<TListDto>> GetPagedAsync(int page, int pageSize, string? search = null)
    {
        Expression<Func<TEntity, bool>>? predicate = null;
        
        // Override in derived classes for custom search logic
        if (!string.IsNullOrEmpty(search))
        {
            predicate = BuildSearchPredicate(search);
        }

        var (entities, totalCount) = await _repository.GetPagedAsync(
            page, 
            pageSize, 
            predicate,
            GetDefaultOrderBy(),
            true);

        var dtos = _mapper.Map<IEnumerable<TListDto>>(entities);
        
        return new PagedResultDto<TListDto>
        {
            Items = dtos,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        };
    }

    public virtual async Task<TDto> CreateAsync(TCreateDto createDto)
    {
        var entity = _mapper.Map<TEntity>(createDto);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<TDto>(createdEntity);
    }

    public virtual async Task<TDto> UpdateAsync(Guid id,TUpdateDto updateDto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new ArgumentException("Entity not found", nameof(id));

        _mapper.Map(updateDto, entity);
        var updatedEntity = await _repository.UpdateAsync(entity);
        return _mapper.Map<TDto>(updatedEntity);
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;

        await _repository.DeleteAsync(entity);
        return true;
    }

    public virtual async Task<bool> ExistsAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity != null;
    }

    public virtual async Task<int> CountAsync()
    {
        return await _repository.CountAsync();
    }

    // Virtual methods for customization in derived classes
    protected virtual Expression<Func<TEntity, bool>>? BuildSearchPredicate(string search)
    {
        // Override in derived classes for entity-specific search logic
        return null;
    }

    protected virtual Expression<Func<TEntity, object>>? GetDefaultOrderBy()
    {
        // Try to get CreatedAt property if it exists (BaseEntity)
        var entityType = typeof(TEntity);
        var createdAtProp = entityType.GetProperty("CreatedAt");
        if (createdAtProp != null)
        {
            var param = Expression.Parameter(entityType, "x");
            var property = Expression.Property(param, createdAtProp);
            var lambda = Expression.Lambda<Func<TEntity, object>>(
                Expression.Convert(property, typeof(object)), param);
            return lambda;
        }
        
        // Fallback to Id property
        var idProp = entityType.GetProperty("Id");
        if (idProp != null)
        {
            var param = Expression.Parameter(entityType, "x");
            var property = Expression.Property(param, idProp);
            var lambda = Expression.Lambda<Func<TEntity, object>>(
                Expression.Convert(property, typeof(object)), param);
            return lambda;
        }
        
        return null;
    }
}