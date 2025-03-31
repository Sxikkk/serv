using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ITokenRepository
{
    Task<RefreshToken> GetTokenByIdAsync(int id);
    Task AddToken(RefreshToken token);
}