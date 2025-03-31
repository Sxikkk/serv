using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int userId);
    Task<User> GetByEmailAsync(string email);
    Task<ICollection<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);

    Task DeleteAsync(int userId);

    Task<bool> ExistsByEmailAsync(string email);

}
