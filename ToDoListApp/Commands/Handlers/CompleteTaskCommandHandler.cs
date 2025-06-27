using Microsoft.EntityFrameworkCore;
using ToDoListApp.Commands.Abstractions;
using ToDoListApp.Commands.Result;
using ToDoListApp.Persistence;

namespace ToDoListApp.Commands.Handlers;

public class CompleteTaskCommandHandler(ToDoContext context) : ICommandHandler<CompleteTaskCommand, DateTime>
{
    public async Task<CommandResult<DateTime>> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var completedTime = DateTime.Now;

        var toDo = await context.ToDos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (toDo is null)
            return CommandResult<DateTime>.Fail(ErrorCode.NotFound, $"Task with id {request.Id} doesn't exist");

        toDo.CompletedDateTime = completedTime;
        context.Update(toDo);
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult<DateTime>.Ok(completedTime);
    }
}