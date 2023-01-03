using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public AuthController(IUserService userService, IJWTService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto userLoginDto)
        {
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
            
            return Ok("Bearer " + token);
        }

    }
}
