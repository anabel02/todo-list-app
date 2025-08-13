using Microsoft.AspNetCore.OData.Query;

namespace ToDoListApp.Application.Abstractions;

public record ODataQuery<TEntity, TProjection>(ODataQueryOptions<TEntity> ODataQueryOptions)
    : BaseODataQuery<TEntity, IQueryable<TProjection>>(ODataQueryOptions);