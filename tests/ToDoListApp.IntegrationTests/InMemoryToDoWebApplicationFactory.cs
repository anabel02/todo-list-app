using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using ToDoListApp.Persistence;

namespace ToDoListApp.IntegrationTests;

public class InMemoryToDoWebApplicationFactory : WebApplicationFactory<Program>
{
    private const string InMemoryDatabaseName = "ToDoTestDb";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, _) => { context.HostingEnvironment.EnvironmentName = "Testing"; });

        builder.ConfigureServices(services =>
        {
            // Remove the DbContext configured for MySQL
            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<ToDoContext>));

            if (dbContextDescriptor != null)
                services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));

            if (dbConnectionDescriptor != null)
                services.Remove(dbConnectionDescriptor);

            // Add in-memory database
            services.AddDbContext<ToDoContext>(options =>
                options.UseInMemoryDatabase(InMemoryDatabaseName));

            // Build the service provider
            var sp = services.BuildServiceProvider();

            // Initialize the database
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ToDoContext>();

            // Reset database for a clean state
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        });
    }
}