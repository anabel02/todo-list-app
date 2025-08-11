using Microsoft.AspNetCore.OData.Query;

namespace ToDoListApp.Application.Abstractions;

public class ODataQuery<TData, TProjection>(ODataQueryOptions<TData> options) : IQuery<TProjection>
{
    public ODataQueryOptions<TData> ODataQueryOptions { get; set; } = options;
}