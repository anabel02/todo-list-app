using ToDoListApp.Application.Abstractions;

namespace ToDoListApp.Application.Commands;

public record CompleteTaskCommand(int Id) : ICommand<DateTime>;