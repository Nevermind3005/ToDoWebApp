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
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IJWTService jwtService, IMapper mapper, IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _mapper = mapper;
            _refreshTokenService = refreshTokenService;
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

            var generatedToken = _refreshTokenService.GenerateRefreshToken(user.Id);
            
            var refreshToken = await _refreshTokenService.AddRefreshToken(generatedToken);

            if (refreshToken is null)
            {
                return Problem();
            }
            
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };
            
            Response.Cookies.Append("refresh_token", refreshToken.Token, options);
            
            Response.Cookies.Append("u_id", user.Id.ToString(), options);
            
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


        [HttpPost("token")]
        public async Task<ActionResult<string>> Token()
        {
            var refreshTokenCookie = Request.Cookies["refresh_token"];
            var userId = Request.Cookies["u_id"];

            var token = await _refreshTokenService.GetRefreshToken(refreshTokenCookie);

            if (token is null)
            {
                return Unauthorized();
            }

            if (token.UserId.ToString() != userId)
            {
                return Unauthorized();
            }

            if (token.Expires < DateTime.UtcNow)
            {
                return Unauthorized();
            }

            var user = await _userService.GetUser(token.UserId);
            
            var jwt = _jwtService.GenerateJWT(user);
            
            var refreshToken = await _refreshTokenService.AddRefreshToken(_refreshTokenService.GenerateRefreshToken(user.Id));

            if (refreshToken is null)
            {
                return Problem();
            }
            
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };
            
            Response.Cookies.Append("refresh_token", refreshToken.Token, options);
            
            Response.Cookies.Append("u_id", user.Id.ToString(), options);
            
            return Ok(jwt);
            
        }

    }
}
