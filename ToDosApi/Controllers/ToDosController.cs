using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using ToDosApi.Models;
using ToDosApi.Persistence;

namespace ToDosApi.Controllers;

public class ToDosController : ControllerBase
{
    private readonly ToDoContext _context;
    public ToDosController(ToDoContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IQueryable<ToDo> Get() => _context.ToDos;
    
    [EnableQuery]
    public ActionResult<ToDo> Get([FromODataUri] int key) 
        => Ok(_context.ToDos.Find(key));
}