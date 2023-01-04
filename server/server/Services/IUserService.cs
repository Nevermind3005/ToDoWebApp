using server.Models;

namespace server.Services;

public interface IUserService
{
    Task<List<User>> GetUsers(bool include = false);
    Task<User> GetUser(long id, bool include = false);
    Task<User> GetUser(string username, bool include = false);
    long? GetUserId();
    Task<User> AddUser(User user);
    Task<bool?> DeleteUser(long id);
}