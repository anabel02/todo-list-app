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

    public static void SeedTodos(this ToDoContext context, params ToDo[] todos)
    {
        context.ToDos.AddRange(todos);
        context.SaveChanges();
    }
}