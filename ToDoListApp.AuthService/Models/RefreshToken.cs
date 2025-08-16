namespace ToDoListApp.AuthService.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Revoked { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}
