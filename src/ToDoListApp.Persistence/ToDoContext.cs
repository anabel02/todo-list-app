using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Domain;

namespace ToDoListApp.Persistence;

public class ToDoContext(DbContextOptions<ToDoContext> options) : DbContext(options)
{
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}