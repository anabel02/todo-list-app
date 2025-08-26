using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class RemoveTaskCommandHandler(ToDoContext context, ICurrentUser currentUser) : ICommandHandler<RemoveTaskCommand>
{
    public async Task<CommandResult> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        var profile = await context.Profiles.SingleOrDefaultAsync(p => p.UserId == currentUser.UserId, cancellationToken);
        if (profile is null)
            return CommandResult.Fail(HttpStatusCode.Forbidden, "Profile not found for this user.");

        var toDo = await context.ToDos.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (toDo is null)
            return CommandResult.Fail(HttpStatusCode.NotFound, "Task not found.");

        if (toDo.ProfileId != profile.Id)
            return CommandResult.Fail(HttpStatusCode.Forbidden, "User does not have permission to complete this task.");

        context.ToDos.Remove(toDo);
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult.Ok();
    }
}