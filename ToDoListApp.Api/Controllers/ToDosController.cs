using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Application.Abstractions;
using ToDoListApp.Application.Commands;
using ToDoListApp.Application.Dtos;
using ToDoListApp.Application.Queries;
using ToDoListApp.Domain;
using ToDoListApp.Helpers;

namespace ToDoListApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDosController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<PaginatedList<ToDoDto>> GetTodos([FromQuery] QueryParameters parameters)
    {
        var query = new GetTodosQuery(parameters);
        var result = await sender.Send(query);
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<ToDo>> Create([FromBody] CreateTaskCommand request)
    {
        var result = await sender.Send(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}/complete")]
    public async Task<ActionResult<DateTime>> Complete([FromRoute] int id)
    {
        var result = await sender.Send(new CompleteTaskCommand(id));
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTaskCommand.UpdateToDoBody body)
    {
        var request = new UpdateTaskCommand(id, body);
        var result = await sender.Send(request);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var result = await sender.Send(new RemoveTaskCommand(id));
        return result.ToActionResult();
    }
}