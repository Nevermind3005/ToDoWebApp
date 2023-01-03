using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;
using BC = BCrypt.Net.BCrypt;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IJWTService _jwtService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IJWTService jwtService, IMapper mapper)
        {
            _userService = userService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.GetUser(userLoginDto.Username);

            if (user is null)
            {
                return NotFound();
            }

            if (!BC.Verify(userLoginDto.Password, user.Password))
            {
                return NotFound();
            }

            var token = _jwtService.GenerateJWT(user);
            
            return Ok(token);

        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<User>(userRegisterDto);
            var userRes = await _userService.AddUser(user);
            if (userRes is null)
            {
                return Problem();
            }
            return Ok();
        }

        
        
        
    }
}
