using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Application.Commands;
using ToDoListApp.Application.Dtos;
using MediatR;
using ToDoListApp.Helpers;

namespace ToDoListApp.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProfilesController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProfileDto>> Create()
    {
        var command = new CreateProfileCommand();
        var result = await sender.Send(command);
        return result.ToActionResult();
    }
}