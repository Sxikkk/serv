using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc.ShoppingCartItems)
            .ThenInclude(sci => sci.Product)
            .Include(u => u.Orders)
            .ThenInclude(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Include(u => u.Reviews)
            .ThenInclude(r => r.Product)
            .FirstOrDefaultAsync(u => u.Id == id);
    }


    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user =  await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<ICollection<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Role)
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc.ShoppingCartItems)
            .ThenInclude(sci => sci.Product)
            .ThenInclude(p => p.Category)  // Добавлено подгружение категории продукта
            .Include(u => u.Orders)
            .ThenInclude(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ThenInclude(p => p.Category)  // Добавлено подгружение категории продукта для заказа
            .Include(u => u.Reviews)
            .ThenInclude(r => r.Product)
            .ThenInclude(p => p.Category)  // Добавлено подгружение категории продукта для отзыва
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        user.UpdatedAt = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
    
    public async Task<bool> ExistsByIdAsync(int userId)
    {
        return await _context.Users.AnyAsync(u => u.Id == userId);
    }
}
