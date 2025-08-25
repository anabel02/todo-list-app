using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.AuthService.Models;

namespace ToDoListApp.AuthService;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext<User, IdentityRole, string>(options)
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
}