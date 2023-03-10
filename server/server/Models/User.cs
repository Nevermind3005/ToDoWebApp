namespace server.Models;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public List<ToDo> ToDos { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
}