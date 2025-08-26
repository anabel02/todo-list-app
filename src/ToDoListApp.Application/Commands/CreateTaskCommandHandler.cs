using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Domain;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class CreateTaskCommandHandler(ToDoContext context, ICurrentUser currentUser) : ICommandHandler<CreateTaskCommand, ToDoDto>
{
    public async Task<CommandResult<ToDoDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var profile = await context.Profiles.SingleOrDefaultAsync(p => p.UserId == currentUser.UserId, cancellationToken);
        if (profile is null)
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.Forbidden, "Profile not found for this user.");

        if (string.IsNullOrWhiteSpace(request.Body.Task))
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.BadRequest, "Task mustn't be null or empty.");

        var toDo = new ToDo
        {
            Task = request.Body.Task,
            CreatedDateTime = DateTime.UtcNow,
            ProfileId = profile.Id
        };

        context.ToDos.Add(toDo);
        await context.SaveChangesAsync(cancellationToken);

        var result = toDo.MapTo<ToDoDto>();
        return CommandResult<ToDoDto>.Ok(result);
    }
}