using ToDosApi.Exceptions;
using ToDosApi.Persistence;
using ToDosApi.Queries;

namespace ToDosApi.Services;

public class ToDoQueryHandler : IToDoQueryHandler
{
    private readonly ToDoContext _toDoContext;

    public ToDoQueryHandler(ToDoContext toDoContext)
    {
        _toDoContext = toDoContext;
    }

    public Task<GetToDoResponse> FindById(int productId)
    {
        var todo = _toDoContext.ToDos?.FindAsync(productId).Result;
        if (todo is null)
        {
            throw new NotFoundException($"ToDo with id {productId} doesn't exist");
        }

        return Task.FromResult(new GetToDoResponse()
        {
            Id = productId,
            CompletedDateTime = todo.CompletedDateTime,
            CreatedDateTime = todo.CreatedDateTime,
            Task = todo.Task
        });
    }

    public async IAsyncEnumerable<GetToDoResponse> All()
    {
        if (_toDoContext.ToDos is null)
        {
            yield break;
        }
        
        foreach (var todo in _toDoContext.ToDos)
        {
            yield return new GetToDoResponse()
            {
                Id = todo.Id,
                CompletedDateTime = todo.CompletedDateTime,
                CreatedDateTime = todo.CreatedDateTime,
                Task = todo.Task
            };
        }
    }

    public async IAsyncEnumerable<GetToDoResponse> FindByName(string name)
    {
        if (_toDoContext.ToDos is null)
        {
            yield break;
        }

        foreach (var todo in _toDoContext.ToDos.Where(todo => todo.Task != null && todo.Task.Contains(name)))
        {
            yield return new GetToDoResponse()
            {
                Id = todo.Id,
                CompletedDateTime = todo.CompletedDateTime,
                CreatedDateTime = todo.CreatedDateTime,
                Task = todo.Task
            };
        }
    }

    public async IAsyncEnumerable<GetToDoResponse> FilterByIsComplete()
    {
        if (_toDoContext.ToDos is null)
        {
            yield break;
        }
        
        foreach (var todo in _toDoContext.ToDos)
        {
            var todoDto = new GetToDoResponse()
            {
                Id = todo.Id,
                CompletedDateTime = todo.CompletedDateTime,
                CreatedDateTime = todo.CreatedDateTime,
                Task = todo.Task
            };

            if (!todoDto.IsCompleted) continue;
            
            yield return todoDto;
        }
    }

    public async IAsyncEnumerable<GetToDoResponse> FilterByIsNotComplete()
    {
        if (_toDoContext.ToDos is null)
        {
            yield break;
        }
        
        foreach (var todo in _toDoContext.ToDos)
        {
            var todoDto = new GetToDoResponse()
            {
                Id = todo.Id,
                CompletedDateTime = todo.CompletedDateTime,
                CreatedDateTime = todo.CreatedDateTime,
                Task = todo.Task
            };

            if (todoDto.IsCompleted) continue;
            
            yield return todoDto;
        }
    }
}