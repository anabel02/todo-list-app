namespace ToDoListApp.Application.Abstractions;

public interface ICurrentUser
{
    string? UserId { get; }
}