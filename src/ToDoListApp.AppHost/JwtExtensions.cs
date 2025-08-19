namespace ToDoListApp.AppHost;

public static class JwtExtensions
{
    public static IResourceBuilder<ProjectResource> WithJwt(
        this IResourceBuilder<ProjectResource> project,
        IResourceBuilder<ProjectResource> audience,
        IResourceBuilder<ProjectResource> issuer)
    {
        return project
            .WithEnvironment("Jwt__Audience", audience.GetEndpoint("http"))
            .WithEnvironment("Jwt__Issuer", issuer.GetEndpoint("http"));
    }
}