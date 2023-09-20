using ToDosApi.Commands;

namespace ToDosApi.Services;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}