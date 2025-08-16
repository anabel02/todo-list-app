using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.AuthService;
using ToDoListApp.AuthService.Models;
using ToDoListApp.AuthService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

var connectionString = builder.Configuration.GetConnectionString("AuthServiceDb");
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

await app.SeedRolesAsync();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();