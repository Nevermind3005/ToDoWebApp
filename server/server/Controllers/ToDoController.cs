using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{

    private readonly IToDoService _toDoService;
    private readonly IMapper _mapper;

    public ToDoController(IToDoService toDoService, IMapper mapper)
    {
        _toDoService = toDoService;
        _mapper = mapper;
    }

    [HttpGet, Authorize]
    public async Task<ActionResult<List<ToDoGetDto>>> GetTodos()
    {
        var todos = await _toDoService.GetToDos();

        if (todos is null)
        {
            return NotFound();
        }

        var toDoDto = _mapper.Map<List<ToDoGetDto>>(todos);

        return Ok(toDoDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoGetDto>> GetToDo(long id)
    {
        var toDo = await _toDoService.GetToDo(id);

        if (toDo is null)
        {
            return NotFound();
        }

        var toDoDto = _mapper.Map<ToDoGetDto>(toDo);

        return Ok(toDoDto);
    }

    [HttpPost]
    public async Task<ActionResult<ToDoGetDto>> AddToDo(ToDoAddDto toDoReq)
    {
        var toDo = _mapper.Map<ToDo>(toDoReq);
        var toDoRes = await _toDoService.AddToDo(toDo);
        if (toDoRes is null)
        {
            return Problem($"{toDoReq.Name} could not be added");
        }

        var toDoDto = _mapper.Map<ToDoGetDto>(toDo);

        return Ok(toDoDto);
    }

}