using MediatR;

namespace ToDoListApp.Application.Abstractions;

public interface IQuery<out TProjection> : IRequest<IQueryable<TProjection>>;