using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Commands;

namespace ToDoListApp.Tests.CommandHandlers;

public class CreateTaskHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateTask_WhenTaskIsValid()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var handler = new CreateTaskHandler(context);
        var commandBody = new CreateTaskCommand.CreateTaskCommandBody("New Task");
        var command = new CreateTaskCommand(commandBody);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("New Task", result.Data.Task);
        Assert.NotEqual(0, result.Data.Id);
        Assert.NotNull(result.Data.CreatedDateTime);

        var savedEntity = await context.ToDos.FirstAsync();
        Assert.Equal("New Task", savedEntity.Task);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Handle_ShouldFail_WhenTaskIsNullOrEmpty(string? invalidTask)
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var handler = new CreateTaskHandler(context);
        var commandBody = new CreateTaskCommand.CreateTaskCommandBody(invalidTask);
        var command = new CreateTaskCommand(commandBody);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.NotNull(result.ErrorMessage);
        Assert.NotEmpty(result.ErrorMessage);
    }
}