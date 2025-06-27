namespace ToDoListApp.Commands;

public record CompleteTaskCommand(int Id) : ICommand<DateTime>;