using server.Models;

namespace server.Services;

public interface IRefreshTokenService
{
    RefreshToken GenerateRefreshToken(long userId);
    Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken> GetRefreshToken(string refreshToken);
}