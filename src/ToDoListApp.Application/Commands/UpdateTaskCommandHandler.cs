using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class UpdateTaskCommandHandler(ToDoContext context, ICurrentUser? currentUser = null) : ICommandHandler<UpdateTaskCommand, ToDoDto>
{
    public async Task<CommandResult<ToDoDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(currentUser?.UserId))
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.Unauthorized, "User is not authorized.");

        var profile = await context.Profiles.SingleOrDefaultAsync(p => p.UserId == currentUser.UserId, cancellationToken);
        if (profile is null)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.Unauthorized, "Profile not found for this user.");

        var toDo = await context.ToDos.SingleOrDefaultAsync(x => x.Id == request.Id && x.ProfileId == profile.Id, cancellationToken);
        if (toDo is null)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.NotFound, $"Task with id {request.Id} doesn't exist");

        if (toDo.CompletedDateTime is not null)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.BadRequest, "You can't update a completed task");

        if (string.IsNullOrWhiteSpace(request.Body.Task))
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.BadRequest, "Task mustn't be null or empty");

        toDo.Task = request.Body.Task;
        context.Update(toDo);
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult<ToDoDto>.Ok(toDo.MapTo<ToDoDto>());
    }
}