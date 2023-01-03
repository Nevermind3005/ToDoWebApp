using IdGen;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using BC = BCrypt.Net.BCrypt;

namespace server.Services;

public class UserService: IUserService
{

    private readonly DataContext _context;
    private readonly IdGenerator _idGenerator;

    public UserService(DataContext context, IdGenerator idGenerator)
    {
        _context = context;
        _idGenerator = idGenerator;
    }

    public async Task<List<User>> GetUsers(bool include = false)
    {
        try
        {
            var users = _context.Users;
            if (include)
            {
                return await users.Include(u => u.ToDos).ToListAsync();
            }

            return await users.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<User> GetUser(long id, bool include = false)
    {
        try
        {
            var user = _context.Users.Where(u => u.Id == id);
            if (include)
            {
                return await user.Include(u => u.ToDos).FirstAsync();
            }

            return await user.FirstAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<User> GetUser(string username)
    {
        try
        {
            var user = await _context.Users.Where(u => u.Username == username).FirstAsync();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<User> AddUser(User user)
    {
        try
        {
            user.Password = BC.HashPassword(user.Password);
            user.Id = _idGenerator.CreateId();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return await _context.Users.FindAsync(user.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool?> DeleteUser(long id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}