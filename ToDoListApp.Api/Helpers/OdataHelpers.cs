using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ToDoListApp.Domain;

namespace ToDoListApp.Helpers;

public static class OdataExtensions
{
    public static IMvcBuilder AddOdataControllers(this IServiceCollection services, string routePrefix = "odata", int maxTop = 100)
    {
        return services
            .AddControllers()
            .AddOData(opt =>
            {
                opt
                    .Filter()
                    .OrderBy()
                    .SetMaxTop(maxTop)
                    .SkipToken()
                    .AddRouteComponents(routePrefix, GetEdmModel());
            });
    }

    private static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        builder.EntitySet<ToDo>("ToDos");

        builder.EntityType<ToDo>()
            .HasKey(t => t.Id);

        return builder.GetEdmModel();
    }
}