using System.Text.Json.Serialization;

namespace ToDoListApp.Application.Dtos;

public class ToDoDto(int id, string task, DateTime? createdDateTime, DateTime? completedDateTime)
{
    [JsonPropertyName("Id")] public int Id { get; init; } = id;

    [JsonPropertyName("Task")] public string Task { get; init; } = task;

    [JsonPropertyName("CreatedDateTime")] public DateTime? CreatedDateTime { get; init; } = createdDateTime;

    [JsonPropertyName("CompletedDateTime")]
    public DateTime? CompletedDateTime { get; init; } = completedDateTime;
}