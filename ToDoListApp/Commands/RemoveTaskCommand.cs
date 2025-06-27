using ToDoListApp.Commands.Abstractions;

namespace ToDoListApp.Commands;

public record RemoveTaskCommand(int Id) : ICommand;