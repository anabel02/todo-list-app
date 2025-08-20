using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;

namespace ToDoListApp.Application.Commands;

public record UpdateTaskBody(string? Task);

public record UpdateTaskCommand(int Id, UpdateTaskBody Body) : ICommand<ToDoDto>;