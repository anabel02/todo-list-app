using ToDoListApp.Application;
using ToDoListApp.Persistence;
using ToDoListApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Services.AddPersistence(builder.Configuration);
}

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddMediatR();

builder.Services.AddOdataControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseRouting();

app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public abstract partial class Program;