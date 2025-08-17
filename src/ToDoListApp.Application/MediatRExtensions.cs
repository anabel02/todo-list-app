using Microsoft.Extensions.DependencyInjection;

namespace ToDoListApp.Application;

public static class MediatRExtensions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatRExtensions).Assembly));
    }
}