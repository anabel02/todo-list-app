using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Commands;
using ToDoListApp.Domain;

namespace ToDoListApp.Tests.CommandHandlers;

public class UpdateTaskCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldUpdateTask_WhenTaskIsValidAndNotCompleted()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var existingTask = new ToDo
        {
            Id = 1,
            Task = "Original Task",
            CompletedDateTime = null
        };
        context.SeedTodos(existingTask);

        var handler = new UpdateTaskCommandHandler(context);
        var body = new UpdateTaskBody("Updated Task");
        var command = new UpdateTaskCommand(existingTask.Id, body);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        var updatedTask = await context.ToDos.FirstAsync(t => t.Id == existingTask.Id);
        Assert.Equal("Updated Task", updatedTask.Task);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenTaskDoesNotExist()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();

        var handler = new UpdateTaskCommandHandler(context);
        var body = new UpdateTaskBody("Some Task");
        var command = new UpdateTaskCommand(999, body);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        Assert.Contains("doesn't exist", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenTaskIsCompleted()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var completedTask = new ToDo
        {
            Id = 1,
            Task = "Completed Task",
            CompletedDateTime = DateTime.Now
        };
        context.SeedTodos(completedTask);

        var handler = new UpdateTaskCommandHandler(context);
        var body = new UpdateTaskBody("Updated Task");
        var command = new UpdateTaskCommand(completedTask.Id, body);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Contains("can't update a completed task", result.ErrorMessage);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Handle_ShouldFail_WhenTaskIsNullOrWhitespace(string? invalidTask)
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var existingTask = new ToDo
        {
            Id = 1,
            Task = "Original Task",
            CompletedDateTime = null
        };
        context.SeedTodos(existingTask);

        var handler = new UpdateTaskCommandHandler(context);
        var body = new UpdateTaskBody(invalidTask);
        var command = new UpdateTaskCommand(existingTask.Id, body);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Contains("mustn't be null or empty", result.ErrorMessage);
    }
}