using ToDoListApp.Application.Abstractions;

namespace ToDoListApp.Application.Commands;

public class UpdateTaskCommand(int id, UpdateTaskCommand.UpdateTaskBody body) : ICommand
{
    public record UpdateTaskBody(string? Task);

    public int Id { get; set; } = id;
    public string? Task { get; set; } = body.Task;
}