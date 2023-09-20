namespace ToDosApi.Commands;

public class CompleteToDo : ICommand
{
    public int Id { get; set; }
    public DateTime CompletedTime { get; set; }
}