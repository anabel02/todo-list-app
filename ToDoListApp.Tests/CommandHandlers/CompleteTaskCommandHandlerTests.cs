using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Commands;
using ToDoListApp.Domain;

namespace ToDoListApp.Tests.CommandHandlers;

public class CompleteTaskCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCompleteTask_WhenTaskExists()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var task = new ToDo
        {
            Id = 1,
            Task = "Test Task",
            CompletedDateTime = null
        };
        context.SeedTodos(task);

        var handler = new CompleteTaskCommandHandler(context);
        var command = new CompleteTaskCommand(task.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        var updatedTask = await context.ToDos.FirstAsync();
        Assert.Equal(1, updatedTask.Id);
        Assert.NotNull(updatedTask.CompletedDateTime);
        Assert.IsType<DateTime>(result.Data);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenTaskDoesNotExist()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var handler = new CompleteTaskCommandHandler(context);
        var command = new CompleteTaskCommand(999);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        Assert.Contains("doesn't exist", result.ErrorMessage);
    }
}