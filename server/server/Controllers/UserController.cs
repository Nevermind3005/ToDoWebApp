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
    public async Task<ActionResult<List<User>>> GetUsers ()
    {
        var users = await _userService.GetUsers();
        
        if (users is null)
        {
            return NotFound();
        }

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(long id)
    {
        var user = await _userService.GetUser(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
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

}