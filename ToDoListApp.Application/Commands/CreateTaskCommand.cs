using ToDoListApp.Application.Abstractions;
using ToDoListApp.Domain;

namespace ToDoListApp.Application.Commands;

public record CreateTaskCommand(string Task) : ICommand<ToDo>;