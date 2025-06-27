using ToDoListApp.Commands;
using ToDoListApp.Commands.Result;
using ToDoListApp.Models;
using ToDoListApp.Persistence;

namespace ToDoListApp.Services;

public class ToDoService(ToDoContext toDoContext)
{
    public async Task<CommandResult<ToDo>> Handle(CreateToDo request)
    {
        if (string.IsNullOrWhiteSpace(request.Task))
            return CommandResult<ToDo>.Fail(ErrorCode.ValidationError, "Task mustn't be null or empty");

        var toDo = new ToDo
        {
            Task = request.Task,
            CreatedDateTime = DateTime.Now
        };

        toDoContext.ToDos.Add(toDo);
        await toDoContext.SaveChangesAsync();

        return CommandResult<ToDo>.Ok(toDo);
    }

    public async Task<CommandResult> Handle(RemoveToDo request)
    {
        var toDo = await toDoContext.ToDos.FindAsync(request.Id);

        if (toDo is null)
            return CommandResult.Fail(ErrorCode.NotFound, $"Task with id {request.Id} doesn't exist");

        toDoContext.ToDos.Remove(toDo);
        await toDoContext.SaveChangesAsync();

        return CommandResult.Ok();
    }

    public async Task<CommandResult<DateTime>> Handle(CompleteToDo request)
    {
        var completedTime = DateTime.Now;

        var toDo = await toDoContext.ToDos.FindAsync(request.Id);
        if (toDo is null)
            return CommandResult<DateTime>.Fail(ErrorCode.NotFound, $"Task with id {request.Id} doesn't exist");

        toDo.CompletedDateTime = completedTime;
        toDoContext.Update(toDo);
        await toDoContext.SaveChangesAsync();

        return CommandResult<DateTime>.Ok(completedTime);
    }

    public async Task<CommandResult> Handle(UpdateToDo request)
    {
        var toDo = await toDoContext.ToDos.FindAsync(request.Id);

        if (toDo is null)
            return CommandResult.Fail(ErrorCode.NotFound, $"Task with id {request.Id} doesn't exist");

        if (toDo.CompletedDateTime is not null)
            return CommandResult.Fail(ErrorCode.ValidationError, "You can't update a completed task");

        if (string.IsNullOrWhiteSpace(request.Task))
            return CommandResult.Fail(ErrorCode.ValidationError, "Task mustn't be null or empty");

        toDo.Task = request.Task;
        toDoContext.Update(toDo);
        await toDoContext.SaveChangesAsync();

        return CommandResult.Ok();
    }
}