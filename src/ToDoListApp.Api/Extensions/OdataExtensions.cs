using Microsoft.AspNetCore.OData;

namespace ToDoListApp.Extensions;

public static class OdataExtensions
{
    public static IMvcBuilder AddOdataControllers(this IServiceCollection services, int maxTop = 100)
    {
        return services
            .AddControllers()
            .AddOData(opt =>
            {
                opt
                    .Filter()
                    .OrderBy()
                    .SetMaxTop(maxTop)
                    .SkipToken();
            });
    }
}