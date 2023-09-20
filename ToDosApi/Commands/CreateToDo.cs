namespace ToDosApi.Commands;

public class CreateToDo : ICommand
{
    public string? Task { get; set; }
    public DateTime CreatedDateTime { get; set; }
}