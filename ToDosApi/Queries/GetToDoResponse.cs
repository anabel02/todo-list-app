namespace ToDosApi.Queries;

public class GetToDoResponse
{
    public int Id { get; set; }
    public string? Task { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime CompletedDateTime { get; set; }
    public bool IsCompleted => CompletedDateTime != DateTime.MinValue;
}