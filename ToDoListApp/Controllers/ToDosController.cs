using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using ToDoListApp.Commands;
using ToDoListApp.Models;
using ToDoListApp.Persistence;

namespace ToDoListApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDosController(IMediator mediator, ToDoContext context) : BaseController
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
    public async Task<ActionResult<ToDo>> CreateToDoAsync([FromBody] CreateTaskCommand request)
    {
        var result = await mediator.Send(request);
        return FromResult(result);
    }

    [HttpPut("{id}/complete")]
    public async Task<ActionResult<DateTime>> CompleteToDoAsync([FromRoute] int id)
    {
        var result = await mediator.Send(new CompleteTaskCommand(id));
        return FromResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateToDoAsync([FromRoute] int id, [FromBody] UpdateTaskCommand.UpdateToDoBody body)
    {
        var request = new UpdateTaskCommand(id, body);
        var result = await mediator.Send(request);
        return FromResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveToDoAsync([FromRoute] int id)
    {
        var result = await mediator.Send(new RemoveTaskCommand(id));
        return FromResult(result);
    }
}