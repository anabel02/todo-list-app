namespace ToDoListApp.Domain;

public class ToDo
{
    public int Id { get; set; }
    public required string Task { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? CompletedDateTime { get; set; }
}