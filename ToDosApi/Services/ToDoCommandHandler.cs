using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;
using ToDosApi.Commands;
using ToDosApi.Exceptions;
using ToDosApi.Models;
using ToDosApi.Persistence;

namespace ToDosApi.Services;

public class ToDoCommandHandler : ICommandHandler<CreateToDo>, 
                                ICommandHandler<RemoveToDo>, 
                                ICommandHandler<CompleteToDo>,
                                ICommandHandler<UpdateToDo>
{
    private readonly ToDoContext _toDoContext;

    public ToDoCommandHandler(ToDoContext toDoContext)
    {
        _toDoContext = toDoContext;
    }

    public async Task HandleAsync(CreateToDo command)
    {
        var todo = new ToDo() { Task = command.Task, CreatedDateTime = command.CreatedTime };
        _toDoContext.ToDos.Add(todo); 
        await _toDoContext.SaveChangesAsync();
    }

    public async Task HandleAsync(RemoveToDo command)
    {
        var todo = await _toDoContext.ToDos.SingleOrDefaultAsync(todo => todo.Id == command.Id);
        if (todo != null)
        {
            _toDoContext.ToDos.Remove(todo);
        }
        else
        {
            throw new NotFoundException($"ToDo with id {command.Id} doesn't exist");
        }
        await _toDoContext.SaveChangesAsync();
    }

    public async Task HandleAsync(CompleteToDo command)
    {
        var todo = await _toDoContext.ToDos.SingleOrDefaultAsync(todo => todo.Id == command.Id);
        if (todo != null)
        {
            todo.CompletedDateTime = command.CompletedTime;
        }
        else
        {
            throw new NotFoundException($"ToDo with id {command.Id} doesn't exist");
        }
        _toDoContext.Update(todo);
        await _toDoContext.SaveChangesAsync();
    }

    public async Task HandleAsync(UpdateToDo command)
    {
        var todo = await _toDoContext.ToDos.SingleOrDefaultAsync(todo => todo.Id == command.Id);
        if (todo is not null)
        {
            if (todo.CompletedDateTime is not null) throw new BadRequestException("You can't update a completed task");
            if (command.Task != null) todo.Task = command.Task;
        }
        else 
        {
            throw new NotFoundException($"ToDo with id {command.Id} doesn't exist");
        }

        _toDoContext.Update(todo);
        await _toDoContext.SaveChangesAsync();
    }
    
    public async Task HandleAsync(int key, Delta<ToDo> delta)
    {
        var todo = await _toDoContext.ToDos.SingleOrDefaultAsync(todo => todo.Id == key);
        if (todo != null)
        {
            delta.Patch(todo);
        }
        else 
        {
            throw new NotFoundException($"ToDo with id {key} doesn't exist");
        }

        await _toDoContext.SaveChangesAsync();
    }
}