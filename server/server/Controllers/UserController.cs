using AutoMapper;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<UserGetDto>> GetUser(long id, bool include)
    {
        var user = await _userService.GetUser(id, include);

        if (user is null)
        {
            return NotFound();
        }

        var userRes = _mapper.Map<UserGetDto>(user);

        return Ok(userRes);
    }

    [HttpPost]
    public async Task<ActionResult<User>> AddUser(UserAddDto userReq)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(userReq);
            var userRes = await _userService.AddUser(user);
            if (userRes is null)
            {
                return Problem();
            }

            return Ok(userRes);
        }

        return BadRequest(ModelState);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(long id)
    {
        var success = await _userService.DeleteUser(id);

        if (success is null)
        {
            return Problem();
        }

        if (success == false)
        {
            return NotFound();
        }

        return NoContent();
    }

}