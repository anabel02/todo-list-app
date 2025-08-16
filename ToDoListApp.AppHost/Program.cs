using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("password", secret: true);

var mysql = builder.AddMySql("mysql", password, 3306).WithLifetime(ContainerLifetime.Persistent);

var todoAppDb = mysql.AddDatabase("todo-db");
var authServiceDb = mysql.AddDatabase("auth-db");

builder.AddProject<ToDoListApp_AuthService>("auth-service")
    .WaitFor(authServiceDb).WithReference(authServiceDb, "AuthServiceDb");

var todoListBackend = builder
    .AddProject<ToDoListApp_Api>("to-do-list-backend")
    .WaitFor(todoAppDb)
    .WithReference(todoAppDb, "ToDoListAppDb");

var reactClient = builder
    .AddNpmApp("to-do-list-react", "../to-dos-app")
    .WithHttpEndpoint(targetPort: 3000)
    .WithEnvironment("REACT_APP_API_URL", todoListBackend.GetEndpoint("http"));

builder.Build().Run();