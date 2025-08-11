using System.Net;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Domain;
using ToDoListApp.Persistence;

namespace ToDoListApp.Application.Commands;

public class CreateToDoHandler(ToDoContext context) : ICommandHandler<CreateTaskCommand, ToDoDto>
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

        var result = new ToDoDto(toDo.Id, toDo.Task, toDo.CreatedDateTime, toDo.CompletedDateTime);
        return CommandResult<ToDoDto>.Ok(result);
    }
}