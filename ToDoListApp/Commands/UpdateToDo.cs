namespace ToDoListApp.Commands;

public class UpdateToDo(int id, UpdateToDo.UpdateToDoBody body) : ICommand
{
    public record UpdateToDoBody(string? Task);

    public int Id { get; set; } = id;
    public string? Task { get; set; } = body.Task;
}