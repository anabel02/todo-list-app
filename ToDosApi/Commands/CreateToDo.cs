namespace ToDosApi.Commands;

public class CreateToDo : ICommand
{
    public string? Task { get; set; }
    public DateTime CreatedTime { get; set; }
}