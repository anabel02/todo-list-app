using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;

namespace ToDoListApp.Application.Commands;

public record CreateTaskCommandBody(string? Task);

public record CreateTaskCommand(CreateTaskCommandBody Body) : ICommand<ToDoDto>;