namespace ToDoListApp.Commands;

public class RemoveToDo(int id) : ICommand
{
    public int Id { get; set; } = id;
}