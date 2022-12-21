namespace server.Models;

public class UserGetDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public List<ToDoGetDto> ToDos { get; set; }
}