using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Commands;

namespace ToDoListApp.Tests.CommandHandlers;

public class CompleteTaskCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCompleteTask_WhenTaskExists()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var (_, user, task) = TestHelpers.CreateUserWithTask(context);
        var handler = new CompleteTaskCommandHandler(context, user);
        var command = new CompleteTaskCommand(task.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        var updatedTask = await context.ToDos.FirstAsync();
        Assert.NotNull(updatedTask.CompletedDateTime);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenTaskDoesNotExist()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var (_, user) = TestHelpers.CreateUser(context);
        var handler = new CompleteTaskCommandHandler(context, user);
        var command = new CompleteTaskCommand(999);

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
        var handler = new CompleteTaskCommandHandler(context, otherUser);
        var command = new CompleteTaskCommand(task.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
    }
}