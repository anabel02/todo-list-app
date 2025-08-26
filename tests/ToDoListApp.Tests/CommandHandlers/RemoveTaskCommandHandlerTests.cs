using System.Net;
using ToDoListApp.Application.Commands;

namespace ToDoListApp.Tests.CommandHandlers;

public class RemoveTaskCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldRemoveTask_WhenTaskExists()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var (_, user, task) = TestHelpers.CreateUserWithTask(context);
        var handler = new RemoveTaskCommandHandler(context, user);
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
        var (_, user) = TestHelpers.CreateUser(context);
        var handler = new RemoveTaskCommandHandler(context, user);
        var command = new RemoveTaskCommand(999);

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
        var handler = new RemoveTaskCommandHandler(context, otherUser);
        var command = new RemoveTaskCommand(task.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
    }
}