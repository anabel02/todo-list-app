using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Commands;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDosCommandsController(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<ToDo>> CreateToDoAsync([FromBody] CreateTaskCommand request)
    {
        var result = await mediator.Send(request);
        return FromResult(result);
    }

    [HttpPut("{id:int}/complete")]
    public async Task<ActionResult<DateTime>> CompleteToDoAsync([FromRoute] int id)
    {
        var result = await mediator.Send(new CompleteTaskCommand(id));
        return FromResult(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateToDoAsync([FromRoute] int id, [FromBody] UpdateTaskCommand.UpdateToDoBody body)
    {
        var request = new UpdateTaskCommand(id, body);
        var result = await mediator.Send(request);
        return FromResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveToDoAsync([FromRoute] int id)
    {
        var result = await mediator.Send(new RemoveTaskCommand(id));
        return FromResult(result);
    }
}