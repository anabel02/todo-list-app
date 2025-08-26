using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Commands;

namespace ToDoListApp.Tests.CommandHandlers;

public class UpdateTaskCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldUpdateTask_WhenTaskIsValidAndNotCompleted()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var (_, currentUser, existingTask) = TestHelpers.CreateUserWithTask(context);
        var handler = new UpdateTaskCommandHandler(context, currentUser);
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
        var (_, user) = TestHelpers.CreateUser(context);
        var handler = new UpdateTaskCommandHandler(context, user);
        var body = new UpdateTaskBody("Some Task");
        var command = new UpdateTaskCommand(999, body);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        Assert.Contains("not found", result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnForbidden_WhenUserDoesNotOwnTask()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var (_, _, task) = TestHelpers.CreateUserWithTask(context, "owner-user", "Owner's Task");
        var (_, otherUser) = TestHelpers.CreateUser(context, "other-user");
        var handler = new UpdateTaskCommandHandler(context, otherUser);
        var body = new UpdateTaskBody("Some Task");
        var command = new UpdateTaskCommand(task.Id, body);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenTaskIsCompleted()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var (_, user, completedTask) = TestHelpers.CreateUserWithTask(context, completed: true);
        var handler = new UpdateTaskCommandHandler(context, user);
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
        var (_, user, existingTask) = TestHelpers.CreateUserWithTask(context);
        var handler = new UpdateTaskCommandHandler(context, user);
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