using Microsoft.EntityFrameworkCore;
using ToDosApi.Models;

namespace ToDosApi.Persistence;

public class ToDoContext : DbContext
{
    public DbSet<ToDo> ToDos { get; set; } = null!;
    
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) {}
}