using ToDoListApp.Commands.Abstractions;

namespace ToDoListApp.Commands;

public record CompleteTaskCommand(int Id) : ICommand<DateTime>;