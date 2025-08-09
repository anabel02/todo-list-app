using Microsoft.EntityFrameworkCore;
using ToDoListApp.Persistence;

namespace ToDoListApp.Helpers;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ToDoContext>();
        context.Database.Migrate();
    }
}