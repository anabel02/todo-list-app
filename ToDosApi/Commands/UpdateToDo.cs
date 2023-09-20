namespace ToDosApi.Commands;

public class UpdateToDo : ICommand
{
    public int Id { get; set; }
    public string? Task { get; set; }
}