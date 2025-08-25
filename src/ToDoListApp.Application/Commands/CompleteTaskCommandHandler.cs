using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class CompleteTaskCommandHandler(ToDoContext context, ICurrentUser? currentUser = null) : ICommandHandler<CompleteTaskCommand, ToDoDto>
{
    public async Task<CommandResult<ToDoDto>> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(currentUser?.UserId))
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.Unauthorized, "User is not authorized.");

        var profile = await context.Profiles.SingleOrDefaultAsync(p => p.UserId == currentUser.UserId, cancellationToken);
        if (profile is null)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.Unauthorized, "Profile not found for this user.");

        var toDo = await context.ToDos.SingleOrDefaultAsync(x => x.Id == request.Id && x.ProfileId == profile.Id, cancellationToken);
        if (toDo is null)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.NotFound, "Task not found for this user.");

        toDo.CompletedDateTime = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult<ToDoDto>.Ok(toDo.MapTo<ToDoDto>());
    }
}