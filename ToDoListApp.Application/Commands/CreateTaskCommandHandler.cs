using System.Net;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Domain;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class CreateTaskHandler(ToDoContext context) : ICommandHandler<CreateTaskCommand, ToDoDto>
{
    public async Task<CommandResult<ToDoDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Task))
            return CommandResult<ToDoDto>.Fail(HttpStatusCode.BadRequest, "Task mustn't be null or empty");

        var toDo = new ToDo
        {
            Task = request.Task,
            CreatedDateTime = DateTime.Now
        };

        context.ToDos.Add(toDo);
        await context.SaveChangesAsync(cancellationToken);

        var result = toDo.MapTo<ToDoDto>();
        return CommandResult<ToDoDto>.Ok(result);
    }
}