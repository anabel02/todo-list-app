namespace ToDoListApp.Commands;

public class CompleteToDo(int id) : ICommand
{
    public int Id { get; set; } = id;
}