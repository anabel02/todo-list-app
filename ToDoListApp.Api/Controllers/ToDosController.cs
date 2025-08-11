using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Commands;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Application.Queries;
using ToDoListApp.Helpers;

namespace ToDoListApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDosController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<PaginatedList<ToDoDto>> Get([FromQuery] QueryParameters parameters)
    {
        var request = new GetTodosQuery(parameters);
        var result = await sender.Send(request);
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<ToDoDto>> Create([FromBody] CreateTaskCommand.CreateTaskCommandBody body)
    {
        var request = new CreateTaskCommand(body);
        var result = await sender.Send(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}/complete")]
    public async Task<ActionResult<DateTime>> Complete([FromRoute] int id)
    {
        var request = new CompleteTaskCommand(id);
        var result = await sender.Send(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTaskCommand.UpdateTaskBody body)
    {
        var request = new UpdateTaskCommand(id, body);
        var result = await sender.Send(request);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var request = new RemoveTaskCommand(id);
        var result = await sender.Send(request);
        return result.ToActionResult();
    }
}