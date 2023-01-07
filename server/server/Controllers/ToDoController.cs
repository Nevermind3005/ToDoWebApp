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
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ToDoController(IToDoService toDoService, IMapper mapper, IUserService userService)
    {
        _toDoService = toDoService;
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet, Authorize]
    public async Task<ActionResult<List<ToDoGetDto>>> GetTodos()
    {
        var userId = _userService.GetUserId();

        if (userId is null)
        {
            return Unauthorized();
        }
        
        var todos = await _toDoService.GetToDosByUser(userId.Value);
    
        if (todos is null)
        {
            return NotFound();
        }
    
        var toDoDto = _mapper.Map<List<ToDoGetDto>>(todos);
    
        return Ok(toDoDto);
    }

    [HttpGet("{id}"), Authorize]
    public async Task<ActionResult<ToDoGetDto>> GetToDo(long id)
    {
        var toDo = await _toDoService.GetToDo(id);

        if (toDo is null)
        {
            return NotFound();
        }

        var userId = _userService.GetUserId();

        if (userId is null)
        {
            return Unauthorized();
        }
        
        if (toDo.UserId != userId.Value)
        {
            return Unauthorized();
        }

        var toDoDto = _mapper.Map<ToDoGetDto>(toDo);

        return Ok(toDoDto);
    }

    [HttpPost, Authorize]
    public async Task<ActionResult<ToDoGetDto>> AddToDo(ToDoAddDto toDoReq)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = _userService.GetUserId();
        
        if (userId is null)
        {
            return Unauthorized();
        }
        
        var toDo = _mapper.Map<ToDo>(toDoReq);
        toDo.UserId = userId.Value;
        var toDoRes = await _toDoService.AddToDo(toDo);
        if (toDoRes is null)
        {
            return Problem($"{toDoReq.Name} could not be added");
        }

        var toDoDto = _mapper.Map<ToDoGetDto>(toDo);

        return Ok(toDoDto);
    }

    [HttpPut("{id}"), Authorize]
    public async Task<ActionResult> UpdateToDo(long id, ToDoUpdateDto toDoUpdateDto)
    {
        //TOTO Refactor
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = _userService.GetUserId();

        if (userId is null)
        {
            return Unauthorized();
        }

        var currentToDo = await _toDoService.GetToDo(id);

        if (currentToDo is null)
        {
            return NotFound();
        }

        if (currentToDo.UserId != userId.Value)
        {
            return Forbid();
        }

        var toDo = _mapper.Map<ToDo>(toDoUpdateDto);

        var success = await _toDoService.UpdateToDo(toDo, id);

        if (success is null)
        {
            return Problem();
        }

        return NoContent();
    }

    [HttpDelete("{id}"), Authorize]
    public async Task<ActionResult> DeleteToDo(long id)
    {
        //TOTO Refactor
        var userId = _userService.GetUserId();

        if (userId is null)
        {
            return Unauthorized();
        }

        var toDo = await _toDoService.GetToDo(id);

        if (toDo is null)
        {
            return NotFound();
        }
        
        if (toDo.UserId != userId)
        {
            return Forbid();
        }

        var success = await _toDoService.DeleteToDo(id);
        
        if (success is null)
        {
            return Problem();
        }

        if (success.Value == false)
        {
            return NotFound();
        }

        return NoContent();
    }

}