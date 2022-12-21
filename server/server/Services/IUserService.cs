using server.Models;

namespace server.Services;

public interface IUserService
{
    Task<List<User>> GetUsers(bool include);
    Task<User> GetUser(long id, bool include);
    Task<User> AddUser(User user);
    Task<bool?> DeleteUser(long id);
}