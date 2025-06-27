using MediatR;
using ToDoListApp.Commands.Result;

namespace ToDoListApp.Commands.Abstractions;

public interface ICommand<T> : IRequest<CommandResult<T>>;

public interface ICommand : IRequest<CommandResult>;