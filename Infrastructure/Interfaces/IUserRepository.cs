using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int userId);
    Task<User> GetUserByEmailAsync(string email);
    Task<ICollection<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);

    Task DeleteAsync(int userId);

    Task<bool> ExistsByEmailAsync(string email);

}
