using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class RemoveTaskCommandHandler(ToDoContext context) : ICommandHandler<RemoveTaskCommand>
{
    public async Task<CommandResult> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        var toDo = await context.ToDos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (toDo is null)
            return CommandResult.Fail(HttpStatusCode.NotFound, $"Task with id {request.Id} doesn't exist");

        context.ToDos.Remove(toDo);
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult.Ok();
    }
}