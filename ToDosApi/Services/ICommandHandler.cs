using ToDosApi.Commands;

namespace ToDosApi.Services;

public interface ICommandHandler<in TCommand, out T> where TCommand : ICommand
{
    T Handle(TCommand command);
}