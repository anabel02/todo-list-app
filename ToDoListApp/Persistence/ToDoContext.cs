using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Persistence;

public class ToDoContext : DbContext
{
    public DbSet<ToDo> ToDos { get; set; } = null!;
    
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) {}
}