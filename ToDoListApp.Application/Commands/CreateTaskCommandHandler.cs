using System.Net;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Domain;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class CreateToDoHandler(ToDoContext context) : ICommandHandler<CreateTaskCommand, ToDo>
{
    public async Task<CommandResult<ToDo>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Task))
            return CommandResult<ToDo>.Fail(HttpStatusCode.BadRequest, "Task mustn't be null or empty");

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