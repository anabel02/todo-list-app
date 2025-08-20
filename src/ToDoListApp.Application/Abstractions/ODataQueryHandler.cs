using Microsoft.EntityFrameworkCore;

namespace ToDoListApp.Application.Abstractions;

public class ODataQueryHandler<TEntity, TProjection>(DbContext context) : IQueryHandler<ODataQuery<TEntity, TProjection>, IQueryable<TProjection>>
    where TEntity : class where TProjection : new()
{
    public virtual async Task<IQueryable<TProjection>> Handle(ODataQuery<TEntity, TProjection> request, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> baseQuery = context.Set<TEntity>();
        var filteredQuery = (IQueryable<TEntity>)request.ODataQueryOptions.ApplyTo(baseQuery);

        var withExtraFilters = request.ExtraFilters.Aggregate(filteredQuery, (current, filter) => current.Where(filter));

        var entities = await withExtraFilters.ToListAsync(cancellationToken: cancellationToken);
        var projection = entities.Select(x => x.MapTo<TProjection>());
        return projection.AsQueryable();
    }
}