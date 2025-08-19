using Projects;
using ToDoListApp.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("password", secret: true);

var mysql = builder.AddMySql("mysql", password, 3306).WithLifetime(ContainerLifetime.Persistent);

var todoAppDb = mysql.AddDatabase("todo-db");
var authServiceDb = mysql.AddDatabase("auth-db");

var authService = builder.AddProject<ToDoListApp_AuthService>("auth-service")
    .WaitFor(authServiceDb)
    .WithReference(authServiceDb, "AuthServiceDb");

var backend = builder
    .AddProject<ToDoListApp_Api>("to-do-list-backend")
    .WaitFor(todoAppDb)
    .WithReference(todoAppDb, "ToDoListAppDb");

authService.WithJwt(
    audience: backend, 
    issuer: authService);

backend.WithJwt(
    audience: backend, 
    issuer: authService);

var angularClient = builder
    .AddNpmApp("todolist-angular", "../../clients/todolist-angular")
    .WithHttpEndpoint(targetPort: 4200)
    .WithEnvironment("NG_APP_API_URL", backend.GetEndpoint("http"))
    .WithEnvironment("NG_APP_AUTH_URL", authService.GetEndpoint("http"));

builder.Build().Run();