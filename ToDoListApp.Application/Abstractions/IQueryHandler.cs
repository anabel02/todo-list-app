using MediatR;

namespace ToDoListApp.Application.Abstractions;

public interface IQueryHandler<in TQuery, TProjection> : IRequestHandler<TQuery, PaginatedList<TProjection>> where TQuery : IQuery<TProjection>;