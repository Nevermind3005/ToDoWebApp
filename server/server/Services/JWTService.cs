using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using server.Models;

namespace server.Services;

public class JWTService : IJWTService
{
    private IConfiguration _configuration;
    
    public JWTService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJWT(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:AccessSecret").Value));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}