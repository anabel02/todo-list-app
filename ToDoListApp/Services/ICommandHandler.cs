using ToDoListApp.Commands;

namespace ToDoListApp.Services;

public interface ICommandHandler<in TCommand, out T> where TCommand : ICommand
{
    T Handle(TCommand command);
}