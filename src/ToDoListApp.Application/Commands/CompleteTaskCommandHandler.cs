using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class CompleteTaskCommandHandler(ToDoContext context, ICurrentUser currentUser) : ICommandHandler<CompleteTaskCommand, ToDoDto>
{
    public async Task<CommandResult<ToDoDto>> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var profile = await context.Profiles.SingleOrDefaultAsync(p => p.UserId == currentUser.UserId, cancellationToken);
        if (profile is null)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.Forbidden, "Profile not found for this user.");

        var toDo = await context.ToDos.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (toDo is null)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.NotFound, "Task not found.");

        if (toDo.ProfileId != profile.Id)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.Forbidden, "User does not have permission to complete this task.");

        toDo.CompletedDateTime = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult<ToDoDto>.Ok(toDo.MapTo<ToDoDto>());
    }
}