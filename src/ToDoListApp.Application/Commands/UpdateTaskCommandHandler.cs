using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class UpdateTaskCommandHandler(ToDoContext context, ICurrentUser currentUser) : ICommandHandler<UpdateTaskCommand, ToDoDto>
{
    public async Task<CommandResult<ToDoDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var profile = await context.Profiles.SingleOrDefaultAsync(p => p.UserId == currentUser.UserId, cancellationToken);
        if (profile is null)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.Forbidden, "Profile not found for this user.");

        var toDo = await context.ToDos.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (toDo is null)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.NotFound, "Task not found.");

        if (toDo.ProfileId != profile.Id)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.Forbidden, "User does not have permission to complete this task.");

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