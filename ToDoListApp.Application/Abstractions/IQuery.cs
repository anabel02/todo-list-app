using MediatR;

namespace ToDoListApp.Application.Abstractions;

public interface IQuery<TProjection> : IRequest<PaginatedList<TProjection>>;

public class Query<TProjection>(QueryParameters parameters) : IQuery<TProjection>
{
    public QueryParameters Parameters { get; set; } = parameters;
}