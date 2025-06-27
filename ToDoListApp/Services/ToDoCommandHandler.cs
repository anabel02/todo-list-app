using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using ToDoListApp.Commands;
using ToDoListApp.Exceptions;
using ToDoListApp.Models;
using ToDoListApp.Persistence;

namespace ToDoListApp.Services;

public class ToDoCommandHandler : ICommandHandler<CreateToDo, Task<ToDo>>, 
                                ICommandHandler<RemoveToDo, Task>, 
                                ICommandHandler<CompleteToDo, Task<DateTime>>,
                                ICommandHandler<UpdateToDo, Task>
{
    private readonly ToDoContext _toDoContext;

    public ToDoCommandHandler(ToDoContext toDoContext)
    
    {
        _toDoContext = toDoContext;
    }

    public async Task<ToDo> Handle(CreateToDo command)
    {
        if (command.Task is null || command.Task.Trim().Length == 0) 
            throw new BadRequestException("Task mustn't be null or empty");
        
        var todo = _toDoContext.ToDos.Add(new ToDo()
        {
            Task = command.Task, 
            CreatedDateTime = DateTime.Now
        });
        
        if (await _toDoContext.SaveChangesAsync() == 0)
            throw new ServerErrorException("Task didn't be created");
        
        return todo.Entity;
    }

    public async Task Handle(RemoveToDo command)
    {
        var todo = await _toDoContext.ToDos.SingleOrDefaultAsync(todo => todo.Id == command.Id);
        if (todo != null)
        {
            _toDoContext.ToDos.Remove(todo);
        }
        else
        {
            throw new NotFoundException($"Task with id {command.Id} doesn't exist");
        }

        await _toDoContext.SaveChangesAsync();
    }

    public async Task<DateTime> Handle(CompleteToDo command)
    {
        var completedTime = DateTime.Now;
        
        var todo = await _toDoContext.ToDos.SingleOrDefaultAsync(todo => todo.Id == command.Id);
        if (todo != null)
        {
            todo.CompletedDateTime = completedTime;
        }
        else
        {
            throw new NotFoundException($"Task with id {command.Id} doesn't exist");
        }
        _toDoContext.Update(todo);
        await _toDoContext.SaveChangesAsync();

        return completedTime;
    }

    public async Task Handle(UpdateToDo command)
    {
        var todo = await _toDoContext.ToDos.SingleOrDefaultAsync(todo => todo.Id == command.Id);
        if (todo is not null)
        {
            if (todo.CompletedDateTime is not null) 
                throw new BadRequestException("You can't update a completed task");
            if (command.Task is null || command.Task.Trim().Length == 0) 
                throw new BadRequestException("Task mustn't be null or empty");
            todo.Task = command.Task;
        }
        else 
        {
            throw new NotFoundException($"Task with id {command.Id} doesn't exist");
        }

        _toDoContext.Update(todo);
        await _toDoContext.SaveChangesAsync();
    }
    
    public async Task Handle(int key, Delta<ToDo> delta)
    {
        var todo = await _toDoContext.ToDos.SingleOrDefaultAsync(todo => todo.Id == key);
        if (todo != null)
        {
            delta.Patch(todo);
        }
        else 
        {
            throw new NotFoundException($"Task with id {key} doesn't exist");
        }

        await _toDoContext.SaveChangesAsync();
    }
}