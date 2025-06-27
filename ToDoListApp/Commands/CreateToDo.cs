namespace ToDoListApp.Commands;

public class CreateToDo : ICommand
{
    public string? Task { get; set; }
}