using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Commands;
using ToDoListApp.Domain;

namespace ToDoListApp.Tests.CommandHandlers;

public class CreateProfileCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnUnauthorized_WhenUserIdIsNull()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var fakeUser = new FakeCurrentUser(userId: null);
        var handler = new CreateProfileCommandHandler(context, fakeUser);
        var command = new CreateProfileCommand();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        Assert.Contains("not authorized", result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Handle_ShouldReturnConflict_WhenProfileAlreadyExists()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var (_, user) = TestHelpers.CreateUser(context);
        var handler = new CreateProfileCommandHandler(context, user);
        var command = new CreateProfileCommand();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
        Assert.Contains("already exists", result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Handle_ShouldCreateProfile_WhenUserIdIsValidAndProfileDoesNotExist()
    {
        // Arrange
        await using var context = TestHelpers.CreateInMemoryContext();
        var user = new FakeCurrentUser("test-user");
        var handler = new CreateProfileCommandHandler(context, user);
        var command = new CreateProfileCommand();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(user.UserId, result.Data.UserId);

        var savedProfile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == user.UserId);
        Assert.NotNull(savedProfile);
        Assert.Equal(user.UserId, savedProfile.UserId);
    }
}