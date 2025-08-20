namespace ToDoListApp.Domain;

public class Profile
{
    public int Id { get; set; }
    public string UserId { get; set; }

    public ICollection<ToDo> ToDos { get; set; } = new List<ToDo>();
}