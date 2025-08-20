using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class RemoveTaskCommandHandler(ToDoContext context, ICurrentUser? currentUser = null) : ICommandHandler<RemoveTaskCommand>
{
    public async Task<CommandResult> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(currentUser?.UserId))
            return CommandResult.Fail(HttpStatusCode.Unauthorized, "User is not authorized.");

        var profile = await context.Profiles.SingleOrDefaultAsync(p => p.UserId == currentUser.UserId, cancellationToken);
        if (profile is null)
            return CommandResult.Fail(HttpStatusCode.Unauthorized, "Profile not found for this user.");

        var toDo = await context.ToDos.SingleOrDefaultAsync(x => x.Id == request.Id && x.ProfileId == profile.Id, cancellationToken);

        if (toDo is null)
            return CommandResult.Fail(HttpStatusCode.NotFound, $"Task with id {request.Id} doesn't exist");

        context.ToDos.Remove(toDo);
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult.Ok();
    }
}