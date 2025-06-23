using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using ToDosApi.Models;
using ToDosApi.Persistence;
using ToDosApi.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var modelBuilder = new ODataConventionModelBuilder();

modelBuilder.EntitySet<ToDo>("ToDos");
builder.Services.AddControllers().AddOData(opt =>
    opt.Select().Count().Filter().Expand().Select().OrderBy().SetMaxTop(50)
        .AddRouteComponents("", modelBuilder.GetEdmModel()));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ToDoCommandHandler>();
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();
}

app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapControllers();

app.Run();