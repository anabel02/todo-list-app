using Microsoft.EntityFrameworkCore;
using ToDoListApp.Commands.Abstractions;
using ToDoListApp.Commands.Result;
using ToDoListApp.Persistence;

namespace ToDoListApp.Commands.Handlers;

public class UpdateTaskCommandHandler(ToDoContext context) : ICommandHandler<UpdateTaskCommand>
{
    public async Task<CommandResult> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var toDo = await context.ToDos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (toDo is null)
            return CommandResult.Fail(ErrorCode.NotFound, $"Task with id {request.Id} doesn't exist");

        if (toDo.CompletedDateTime is not null)
            return CommandResult.Fail(ErrorCode.ValidationError, "You can't update a completed task");

        if (string.IsNullOrWhiteSpace(request.Task))
            return CommandResult.Fail(ErrorCode.ValidationError, "Task mustn't be null or empty");

        toDo.Task = request.Task;
        context.Update(toDo);
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult.Ok();
    }
}