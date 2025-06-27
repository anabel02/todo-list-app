namespace ToDoListApp.Commands;

public class UpdateTaskCommand(int id, UpdateTaskCommand.UpdateToDoBody body) : ICommand
{
    public record UpdateToDoBody(string? Task);

    public int Id { get; set; } = id;
    public string? Task { get; set; } = body.Task;
}