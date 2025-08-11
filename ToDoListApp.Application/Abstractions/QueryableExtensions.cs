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

        Expression? combinedExpression = null;

        // --- Search term ---
        if (!string.IsNullOrWhiteSpace(filter.SearchTerm) && searchableFields.Length > 0)
        {
            var term = filter.SearchTerm.ToLower().Trim();
            Expression? searchExpression = null;

            foreach (var selector in searchableFields)
            {
                var replacedBody = new ParameterReplacer(selector.Parameters[0], parameter)
                    .Visit(selector.Body);

                var likeMethod = typeof(DbFunctionsExtensions).GetMethod(
                    nameof(DbFunctionsExtensions.Like),
                    new[] { typeof(DbFunctions), typeof(string), typeof(string) }
                )!;
                var efFunctions = Expression.Property(null, typeof(EF), nameof(EF.Functions));
                var toLower = typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!;

                var toLowerCall = Expression.Call(replacedBody!, toLower);
                var pattern = Expression.Constant($"%{term}%");
                var likeCall = Expression.Call(null, likeMethod, efFunctions, toLowerCall, pattern);

                searchExpression = searchExpression == null
                    ? likeCall
                    : Expression.OrElse(searchExpression, likeCall);
            }

            combinedExpression = searchExpression;
        }

        // --- Field filters ---
        foreach (var ff in filter.GetFilters())
        {
            var property = Expression.Property(parameter, ff.FieldName);

            Expression comparison;

            if (ff.Value == null || ff.Value == DBNull.Value)
            {
                // For null checks, don't use Constant with a value type
                comparison = ff.Operator switch
                {
                    FilterOperator.Equal => Expression.Equal(property, Expression.Constant(null, property.Type)),
                    FilterOperator.NotEqual => Expression.NotEqual(property, Expression.Constant(null, property.Type)),
                    _ => throw new NotSupportedException($"Operator {ff.Operator} not supported for null values")
                };
            }
            else
            {
                // Normal constant for non-null values
                var constant = Expression.Constant(Convert.ChangeType(ff.Value, property.Type));
                comparison = ff.Operator switch
                {
                    FilterOperator.Equal => Expression.Equal(property, constant),
                    FilterOperator.NotEqual => Expression.NotEqual(property, constant),
                    _ => throw new NotSupportedException($"Operator {ff.Operator} not supported")
                };
            }

            combinedExpression = combinedExpression == null
                ? comparison
                : Expression.AndAlso(combinedExpression, comparison);
        }


        // --- Apply to query ---
        if (combinedExpression != null)
        {
            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
            query = query.Where(lambda);
        }

        return query;
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