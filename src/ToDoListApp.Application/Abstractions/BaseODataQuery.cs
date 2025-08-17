using System.Linq.Expressions;
using Microsoft.AspNetCore.OData.Query;

namespace ToDoListApp.Application.Abstractions;

public record BaseODataQuery<TEntity, TResponse>(ODataQueryOptions<TEntity> ODataQueryOptions) : IQuery<TResponse>
{
    private readonly List<Expression<Func<TEntity, bool>>> _extraFilters = [];
    internal IReadOnlyList<Expression<Func<TEntity, bool>>> ExtraFilters => _extraFilters;

    internal void AddFilter(Expression<Func<TEntity, bool>> filter) => _extraFilters.Add(filter);
}