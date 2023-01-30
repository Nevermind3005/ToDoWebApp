using System.Security.Cryptography;
using IdGen;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace server.Services;

public class RefreshTokenService : IRefreshTokenService
{
    
    private readonly IdGenerator _idGenerator;
    private readonly DataContext _context;

    public RefreshTokenService(IdGenerator idGenerator, DataContext context)
    {
        _idGenerator = idGenerator;
        _context = context;
    }


    public RefreshToken GenerateRefreshToken(long userId)
    {
        var refreshToken = new RefreshToken
        {
            Id = _idGenerator.CreateId(),
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            CreatedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = userId
        };
        return refreshToken;
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        try
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            return await _context.RefreshTokens.FindAsync(refreshToken.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<RefreshToken> GetRefreshToken(string refreshToken)
    {
        try
        {
            return await _context.RefreshTokens.Where(r => r.Token == refreshToken).FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}