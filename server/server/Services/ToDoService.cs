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

    public async Task<ToDo> AddToDo(ToDoAddDto toDoDto)
    {
        try
        {
            var toDo = new ToDo
            {
                Id = _idGenerator.CreateId(),
                Name = toDoDto.Name,
                Description = toDoDto.Description
            };
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
}