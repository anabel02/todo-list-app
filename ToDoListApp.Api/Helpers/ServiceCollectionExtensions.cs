using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using ToDoListApp.Domain;

namespace ToDoListApp.Helpers;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddOData(this IServiceCollection services)
    {
        var modelBuilder = new ODataConventionModelBuilder();

        modelBuilder.EntitySet<ToDo>("ToDos");
        return services.AddControllers().AddOData(opt =>
            opt.Select().Count().Filter().Expand().Select().OrderBy().SetMaxTop(50)
                .AddRouteComponents("", modelBuilder.GetEdmModel()));
    }
}