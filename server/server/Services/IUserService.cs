using server.Models;

namespace server.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(long id);
    Task<User> AddUser(User user);
}