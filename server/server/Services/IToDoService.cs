using server.Models;

namespace server.Services;

public interface IToDoService
{
    Task<List<ToDo>> GetToDos();
    Task<ToDo> GetToDo(long id);
    Task<ToDo> AddToDo(ToDo toDo);
}