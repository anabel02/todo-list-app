using Microsoft.EntityFrameworkCore;

namespace ToDosApi.Persistence;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ToDoContext>();
        context.Database.Migrate();
    }
}