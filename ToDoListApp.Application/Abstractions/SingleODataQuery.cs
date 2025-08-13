using Microsoft.AspNetCore.OData.Query;

namespace ToDoListApp.Application.Abstractions;

public record SingleODataQuery<TEntity, TProjection>(ODataQueryOptions<TEntity> ODataQueryOptions)
    : BaseODataQuery<TEntity, TProjection?>(ODataQueryOptions);