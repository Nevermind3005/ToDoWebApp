using IdGen;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace server.Services;

public class ToDoService: IToDoService
{

    private readonly DataContext _context;
    private readonly IdGenerator _idGenerator;

    public ToDoService(DataContext context, IdGenerator idGenerator)
    {
        _context = context;
        _idGenerator = idGenerator;
    }


    public async Task<List<ToDo>> GetToDos()
    {
        try
        {
            return await _context.ToDos.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<List<ToDo>> GetToDosByUser(long id)
    {
        try
        {
            return await _context.ToDos.Where(t => t.UserId == id).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw null;
        }
    }

    public async Task<ToDo> GetToDo(long id)
    {
        try
        {
            return await _context.ToDos.FindAsync(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<ToDo> AddToDo(ToDo toDo)
    {
        try
        {
            toDo.Id = _idGenerator.CreateId();
            await _context.ToDos.AddAsync(toDo);
            await _context.SaveChangesAsync();
            return await _context.ToDos.FindAsync(toDo.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
        
    }

    public async Task<bool?> DeleteToDo(long id)
    {
        try
        {
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo is null)
            {
                return false;
            }

            _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool?> UpdateToDo(ToDo toDo, long id)
    {
        try
        {
            var currentToDo = await _context.ToDos.FindAsync(id);
            if (currentToDo is null)
            {
                return false;
            }

            currentToDo.Name = toDo.Name;
            currentToDo.Description = toDo.Description;
            currentToDo.CompletedAt = toDo.CompletedAt;

            _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}