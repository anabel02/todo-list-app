using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class CompleteTaskCommandHandler(ToDoContext context) : ICommandHandler<CompleteTaskCommand, DateTime>
{
    public async Task<CommandResult<DateTime>> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var completedTime = DateTime.UtcNow;

        var toDo = await context.ToDos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (toDo is null)
            return CommandResult<DateTime>.Fail(HttpStatusCode.NotFound, $"Task with id {request.Id} doesn't exist");

        toDo.CompletedDateTime = completedTime;
        context.Update(toDo);
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult<DateTime>.Ok(completedTime);
    }
}