using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("password", "root", secret: true);

var mysql = builder.AddMySql("mysql", password, port: 3306)
    .WithLifetime(ContainerLifetime.Persistent);

var db = mysql.AddDatabase("database");

var todoListBackend = builder
    .AddProject<ToDosApi>("to-do-list-backend")
    .WaitFor(db)
    .WithReference(db, "DefaultConnection");

var reactClient = builder
    .AddNpmApp("to-do-list-react", "../to-dos-app")
    .WithHttpEndpoint(targetPort: 3000)
    .WithEnvironment("REACT_APP_API_URL", todoListBackend.GetEndpoint("http"));

builder.Build().Run();