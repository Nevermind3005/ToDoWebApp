namespace server.Models;

public class ToDoAddDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long UserId { get; set; }
}