using MediatR;

namespace ToDoListApp.Application.Abstractions;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, CommandResult<TResult>> where TCommand : ICommand<TResult>;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, CommandResult> where TCommand : ICommand;