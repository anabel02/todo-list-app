using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class UpdateTaskCommandHandler(ToDoContext context) : ICommandHandler<UpdateTaskCommand>
{
    public async Task<CommandResult> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var toDo = await context.ToDos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (toDo is null)
            return CommandResult.Fail(HttpStatusCode.NotFound, $"Task with id {request.Id} doesn't exist");

        if (toDo.CompletedDateTime is not null)
            return CommandResult.Fail(HttpStatusCode.BadRequest, "You can't update a completed task");

        if (string.IsNullOrWhiteSpace(request.Body.Task))
            return CommandResult.Fail(HttpStatusCode.BadRequest, "Task mustn't be null or empty");

        toDo.Task = request.Body.Task;
        context.Update(toDo);
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult.Ok();
    }
}