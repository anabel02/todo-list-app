using System.Reflection;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using ToDoListApp.Models;

namespace ToDoListApp;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    public static IMvcBuilder AddOData(this IServiceCollection services)
    {
        var modelBuilder = new ODataConventionModelBuilder();

        modelBuilder.EntitySet<ToDo>("ToDos");
        return services.AddControllers().AddOData(opt =>
            opt.Select().Count().Filter().Expand().Select().OrderBy().SetMaxTop(50)
                .AddRouteComponents("", modelBuilder.GetEdmModel()));
    }
}