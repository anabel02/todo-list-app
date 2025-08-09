using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoListApp.Domain;

public class ToDo
{
    [JsonPropertyName("Id")] public int Id { get; set; }

    [JsonPropertyName("Task")] public string Task { get; set; } = null!;

    [JsonPropertyName("CreatedDateTime")] public DateTime? CreatedDateTime { get; set; }

    [JsonPropertyName("CompletedDateTime")]
    public DateTime? CompletedDateTime { get; set; }
}