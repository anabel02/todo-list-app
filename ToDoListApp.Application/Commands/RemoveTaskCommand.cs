using ToDoListApp.Application.Abstractions;

namespace ToDoListApp.Application.Commands;

public record RemoveTaskCommand(int Id) : ICommand;