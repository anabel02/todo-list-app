using Microsoft.EntityFrameworkCore;
using ToDoListApp.Domain;
using ToDoListApp.Persistence;

namespace ToDoListApp.Tests;

public static class TestHelpers
{
    public static ToDoContext CreateInMemoryContext(string? dbName = null)
    {
        var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseInMemoryDatabase(dbName ?? Guid.NewGuid().ToString())
            .Options;

        return new ToDoContext(options);
    }

    private static void SeedTodos(this ToDoContext context, params ToDo[] todos)
    {
        context.ToDos.AddRange(todos);
        context.SaveChanges();
    }

    private static void SeedProfiles(this ToDoContext context, params Profile[] profiles)
    {
        context.Profiles.AddRange(profiles);
        context.SaveChanges();
    }

    private static Profile CreateProfile(string userUid, string? name = null)
    {
        return new Profile
        {
            UserId = userUid
        };
    }

    private static ToDo CreateTodo(Profile profile, string title = "Test Todo", DateTime? completedDate = null)
    {
        return new ToDo
        {
            ProfileId = profile.Id,
            Task = title,
            CompletedDateTime = completedDate
        };
    }

    public static (Profile profile, FakeCurrentUser currentUser) CreateUser(ToDoContext context, string userId = "test-user")
    {
        var profile = CreateProfile(userId);
        context.SeedProfiles(profile);

        var currentUser = new FakeCurrentUser(userId);
        return (profile, currentUser);
    }

    public static (Profile profile, FakeCurrentUser currentUser, ToDo todo) CreateUserWithTask(
        ToDoContext context,
        string userId = "test-user",
        string todoTitle = "New Task",
        bool completed = false)
    {
        var profile = CreateProfile(userId);
        context.SeedProfiles(profile);

        var currentUser = new FakeCurrentUser(userId);

        var todo = CreateTodo(profile, todoTitle, completed ? DateTime.Now : null);
        context.SeedTodos(todo);

        return (profile, currentUser, todo);
    }
}