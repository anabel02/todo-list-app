using MediatR;

namespace ToDoListApp.Application.Abstractions;

public interface ICommand<T> : IRequest<CommandResult<T>>;

public interface ICommand : IRequest<CommandResult>;