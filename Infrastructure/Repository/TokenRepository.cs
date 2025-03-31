using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class TokenRepository: ITokenRepository
{
    private readonly AppDbContext _context;

    public TokenRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<RefreshToken> GetTokenByIdAsync(int id)
    {
        var token = await _context.RefreshToken.FirstOrDefaultAsync(t => t.UserId == id);
        return token;
    }

    public async Task AddTokenAsync(RefreshToken token)
    {
        var existingToken = await _context.RefreshToken
            .FirstOrDefaultAsync(rt => rt.UserId == token.UserId);
            
        if (existingToken != null)
        {
            _context.RefreshToken.Remove(existingToken);
            await _context.SaveChangesAsync();
        }

        await _context.RefreshToken.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTokenAsync(int userId, string newToken)
    {
        var token = await GetTokenByIdAsync(userId);
        token.Token = newToken;
        await _context.SaveChangesAsync();
    }
}