using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using ToDoListApp.Commands;
using ToDoListApp.Models;
using ToDoListApp.Persistence;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDosController(ToDoContext context, ToDoService toDoService) : BaseController
{
    [EnableQuery]
    public IQueryable<ToDo> Get() => context.ToDos;

    [EnableQuery]
    public ActionResult<ToDo> Get([FromODataUri] int key)
    {
        var todo = context.ToDos.Find(key);
        return todo is not null ? Ok(todo) : NotFound();
    }
    
    [HttpPost]
    public async Task<ActionResult<ToDo>> CreateToDoAsync([FromBody] CreateToDo request)
    {
        var result = await toDoService.Handle(request);
        return FromResult(result);
    }

    [HttpPut("{id}/complete")]
    public async Task<ActionResult<DateTime>> CompleteToDoAsync([FromRoute] int id)
    {
        var request = new CompleteToDo(id);
        var result = await toDoService.Handle(request);
        return FromResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateToDoAsync([FromRoute] int id, [FromBody] UpdateToDo.UpdateToDoBody body)
    {
        var request = new UpdateToDo(id, body);
        var result = await toDoService.Handle(request);
        return FromResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveToDoAsync([FromRoute] int id)
    {
        var request = new RemoveToDo(id);
        var result = await toDoService.Handle(request);
        return FromResult(result);
    }
}