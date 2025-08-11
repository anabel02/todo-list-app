using Microsoft.EntityFrameworkCore;

namespace ToDoListApp.Application.Abstractions;

public class ODataQueryHandler<TEntity, TDto>(DbContext context) : IQueryHandler<ODataQuery<TEntity, TDto>, TDto>
    where TEntity : class where TDto : new()
{
    public virtual async Task<IQueryable<TDto>> Handle(ODataQuery<TEntity, TDto> query, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> baseQuery = context.Set<TEntity>();
        var filteredQuery = (IQueryable<TEntity>)query.ODataQueryOptions.ApplyTo(baseQuery);

        var entities = await filteredQuery.ToListAsync(cancellationToken: cancellationToken);
        var projection = entities.Select(x => x.MapTo<TDto>());
        return projection.AsQueryable();
    }
}