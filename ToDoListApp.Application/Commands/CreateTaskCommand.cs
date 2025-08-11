using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;

namespace ToDoListApp.Application.Commands;

public class CreateTaskCommand(CreateTaskCommand.CreateTaskCommandBody body) : ICommand<ToDoDto>
{
    public record CreateTaskCommandBody(string? Task);

    public string? Task { get; set; } = body.Task;
}