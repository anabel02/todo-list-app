using Microsoft.EntityFrameworkCore;

namespace ToDoListApp.Application.Abstractions;

public class SingleODataQueryHandler<TEntity, TProjection>(DbContext context) : IQueryHandler<SingleODataQuery<TEntity, TProjection>, TProjection?>
    where TEntity : class where TProjection : new()
{
    public async Task<TProjection?> Handle(SingleODataQuery<TEntity, TProjection> request, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> baseQuery = context.Set<TEntity>();
        var filteredQuery = (IQueryable<TEntity>)request.ODataQueryOptions.ApplyTo(baseQuery);

        var entities = request.ExtraFilters.Aggregate(filteredQuery, (current, filter) => current.Where(filter));
        var entity = await entities.FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            return default;

        var projection = entity.MapTo<TProjection>();
        return projection;
    }
}