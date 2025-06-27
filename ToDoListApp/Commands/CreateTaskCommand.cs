using ToDoListApp.Models;

namespace ToDoListApp.Commands;

public record CreateTaskCommand(string Task) : ICommand<ToDo>;