using server.Models;

namespace server.Services;

public interface IJWTService
{
    string GenerateJWT(User user);
}