using IdGen;

namespace server.Models;

public class ToDo
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; } = null;
    public long UserId { get; set; }
    public User User { get; set; }
}