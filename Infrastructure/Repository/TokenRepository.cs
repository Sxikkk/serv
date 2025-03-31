using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repository;

public class TokenRepository: ITokenRepository
{
    private readonly AppDbContext _context;

    public TokenRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Task<RefreshToken> GetTokenByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddToken(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }
}