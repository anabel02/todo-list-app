using System.Text.Json.Serialization;

namespace ToDoListApp.Application.Dtos;

public class ToDoDto
{
    [JsonPropertyName("Id")] public int Id { get; init; }

    [JsonPropertyName("Task")] public string Task { get; init; } = null!;

    [JsonPropertyName("CreatedDateTime")] public DateTime? CreatedDateTime { get; init; }

    [JsonPropertyName("CompletedDateTime")]
    public DateTime? CompletedDateTime { get; init; }
}