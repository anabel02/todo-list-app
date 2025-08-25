using System.Text.Json.Serialization;

namespace ToDoListApp.Application.Dtos;

public class ToDoDto
{
    [JsonPropertyName("id")] public int Id { get; init; }

    [JsonPropertyName("task")] public string Task { get; init; } = null!;

    [JsonPropertyName("createdDateTime")] public DateTime? CreatedDateTime { get; init; }

    [JsonPropertyName("completedDateTime")]
    public DateTime? CompletedDateTime { get; init; }
}