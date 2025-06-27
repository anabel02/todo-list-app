using ToDoListApp.Commands;
using ToDoListApp.Commands.Result;
using ToDoListApp.Models;
using ToDoListApp.Persistence;

namespace ToDoListApp.Handlers;

public class CreateToDoHandler(ToDoContext context) : ICommandHandler<CreateTaskCommand, ToDo>
{
    public async Task<CommandResult<ToDo>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Task))
            return CommandResult<ToDo>.Fail(ErrorCode.ValidationError, "Task mustn't be null or empty");

        var toDo = new ToDo
        {
            Task = request.Task,
            CreatedDateTime = DateTime.Now
        };

        context.ToDos.Add(toDo);
        await context.SaveChangesAsync(cancellationToken);

        return CommandResult<ToDo>.Ok(toDo);
    }
}