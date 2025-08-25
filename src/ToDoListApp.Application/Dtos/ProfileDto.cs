using System.Text.Json.Serialization;

namespace ToDoListApp.Application.Dtos;

public class ProfileDto
{
    [JsonPropertyName("id")] public int Id { get; init; }
    [JsonPropertyName("userId")] public string UserId { get; init; } = null!;
}