using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<UserGetDto>>> GetUsers (bool include)
    {
        var users = await _userService.GetUsers(include);
        
        if (users is null)
        {
            return NotFound();
        }

        var usersRes = _mapper.Map<List<UserGetDto>>(users);

        return Ok(usersRes);
    }

    [HttpGet("me"), Authorize]
    public async Task<ActionResult<UserGetDto>> GetUserMe(bool include)
    {
        var id = _userService.GetUserId();
        if (id is null)
        {
            return Unauthorized();
        }

        var user = await _userService.GetUser(id.Value, include);

        if (user is null)
        {
            return NotFound();
        }
        
        var userRes = _mapper.Map<UserGetDto>(user);
        
        return userRes;
    }

    [HttpGet("{username}"), Authorize]
    public async Task<ActionResult<UserGetDto>> GetUser(string username, bool include)
    {
        var user = await _userService.GetUser(username, include);

        if (user is null)
        {
            return NotFound();
        }

        var userRes = _mapper.Map<UserGetDto>(user);

        return Ok(userRes);
    }

    [HttpDelete, Authorize]
    public async Task<ActionResult> DeleteUser()
    {
        var userId = _userService.GetUserId();

        if (userId is null)
        {
            return Unauthorized();
        }
        
        var success = await _userService.DeleteUser(userId.Value);

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