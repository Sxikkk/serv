using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ITokenRepository
{
    Task<RefreshToken> GetTokenByIdAsync(int userId);
    Task AddTokenAsync(RefreshToken token);
    Task UpdateTokenAsync(int userId, string newToken);
}