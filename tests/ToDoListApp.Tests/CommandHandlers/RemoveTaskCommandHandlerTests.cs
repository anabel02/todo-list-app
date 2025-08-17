using System.Net;
using ToDoListApp.Application.Commands;
using ToDoListApp.Domain;

namespace ToDoListApp.Tests.CommandHandlers;

public class RemoveTaskCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldRemoveTask_WhenTaskExists()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var task = new ToDo
        {
            Id = 1,
            Task = "Task to remove"
        };
        context.SeedTodos(task);

        var handler = new RemoveTaskCommandHandler(context);
        var command = new RemoveTaskCommand(task.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Empty(context.ToDos);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenTaskDoesNotExist()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();

        var handler = new RemoveTaskCommandHandler(context);
        var command = new RemoveTaskCommand(999);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        Assert.Contains("doesn't exist", result.ErrorMessage);
    }
}