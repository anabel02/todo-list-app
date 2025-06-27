using Microsoft.EntityFrameworkCore;
using ToDoListApp.Commands;
using ToDoListApp.Commands.Result;
using ToDoListApp.Persistence;

namespace ToDoListApp.Handlers;

public class RemoveTaskCommandHandler(ToDoContext context) : ICommandHandler<RemoveTaskCommand>
{
    public async Task<CommandResult> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        var toDo = await context.ToDos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (toDo is null)
            return CommandResult.Fail(ErrorCode.NotFound, $"Task with id {request.Id} doesn't exist");

        context.ToDos.Remove(toDo);
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult.Ok();
    }
}