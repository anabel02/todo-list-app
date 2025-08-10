using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ToDoListApp.Application.Abstractions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyFiltering<T>(
        this IQueryable<T> query,
        FilterParams filter,
        params Expression<Func<T, string>>[] searchableFields)
    {
        var parameter = Expression.Parameter(typeof(T), "entity");

        if (string.IsNullOrWhiteSpace(filter.SearchTerm) || searchableFields.Length == 0)
            return query;

        var term = filter.SearchTerm.ToLower().Trim();

        Expression? searchExpression = null;

        foreach (var selector in searchableFields)
        {
            var replacedBody = new ParameterReplacer(selector.Parameters[0], parameter).Visit(selector.Body);

            var likeMethod = typeof(DbFunctionsExtensions).GetMethod(
                nameof(DbFunctionsExtensions.Like),
                [typeof(DbFunctions), typeof(string), typeof(string)]
            )!;

            var efFunctionsProperty = Expression.Property(null, typeof(EF), nameof(EF.Functions));
            var toLowerMethod = typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!;

            var toLowerCall = Expression.Call(replacedBody, toLowerMethod);
            var pattern = Expression.Constant($"%{term}%");
            var likeCall = Expression.Call(null, likeMethod, efFunctionsProperty, toLowerCall, pattern);

            searchExpression = searchExpression == null ? likeCall : Expression.OrElse(searchExpression, likeCall);
        }

        if (searchExpression == null)
            return query;

        var lambda = Expression.Lambda<Func<T, bool>>(searchExpression, parameter);
        return query.Where(lambda);
    }


    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, SortingParams sorting)
    {
        if (string.IsNullOrWhiteSpace(sorting.OrderBy))
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, sorting.OrderBy);
        var lambda = Expression.Lambda(property, parameter);

        var methodName = sorting.Descending ? "OrderByDescending" : "OrderBy";
        var method = typeof(Queryable).GetMethods()
            .First(m => m.Name == methodName && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), property.Type);

        var result = method.Invoke(null, [query, lambda]);
        return (IQueryable<T>)result!;
    }

    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}

public class ParameterReplacer(ParameterExpression oldParameter, ParameterExpression newParameter) : ExpressionVisitor
{
    protected override Expression VisitParameter(ParameterExpression node)
    {
        return node == oldParameter ? newParameter : base.VisitParameter(node);
    }
}