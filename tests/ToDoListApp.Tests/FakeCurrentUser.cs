using ToDoListApp.Application.Abstractions;

namespace ToDoListApp.Tests;

public class FakeCurrentUser(string? userId) : ICurrentUser
{
    public string? UserId { get; } = userId;
}