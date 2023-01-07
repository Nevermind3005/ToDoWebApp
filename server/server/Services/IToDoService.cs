using server.Models;

namespace server.Services;

public interface IToDoService
{
    Task<List<ToDo>> GetToDos();
    Task<List<ToDo>> GetToDosByUser(long id);
    Task<ToDo> GetToDo(long id);
    Task<ToDo> AddToDo(ToDo toDo);
    Task<bool?> DeleteToDo(long id);
    Task<bool?> UpdateToDo(ToDo toDo, long id);
}