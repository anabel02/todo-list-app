using System.Linq.Expressions;

namespace ToDoListApp.Application.Abstractions;

public static class ODataQueryExtensions
{
    public static SingleODataQuery<TEntity, TProjection> WithFilter<TEntity, TProjection>(
        this SingleODataQuery<TEntity, TProjection> query,
        Expression<Func<TEntity, bool>> predicate)
    {
        query.AddFilter(predicate);
        return query;
    }

    public static ODataQuery<TEntity, TProjection> WithFilter<TEntity, TProjection>(
        this ODataQuery<TEntity, TProjection> query,
        Expression<Func<TEntity, bool>> predicate)
    {
        query.AddFilter(predicate);
        return query;
    }
}