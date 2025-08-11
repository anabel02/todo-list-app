using MediatR;

namespace ToDoListApp.Application.Abstractions;

public interface IQueryHandler<in TQuery, TProjection> : IRequestHandler<TQuery, IQueryable<TProjection>> where TQuery : IQuery<TProjection>;