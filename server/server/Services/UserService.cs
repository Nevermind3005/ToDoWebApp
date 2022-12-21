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

    public async Task<List<User>> GetUsers()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<User> GetUser(long id)
    {
        try
        {
            return await _context.Users.FindAsync(id);
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
}