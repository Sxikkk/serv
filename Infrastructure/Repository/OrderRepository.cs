using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class OrderRepository: IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Order>> GetOrdersAsync()
    {
        return await _context.Orders
            .Include(u => u.User)
            .Include(ot => ot.OrderItems)
            .ThenInclude(pr => pr.Product)
            .ThenInclude(ct => ct.Category)
            .ToListAsync();
    }

    public async Task<Order> GetOrderByOrderIdAsync(int orderId)
    {
        return (await _context.Orders
            .Include(u => u.User)
            .Include(ot => ot.OrderItems)
            .ThenInclude(pr => pr.Product)
            .ThenInclude(ct => ct.Category)
            .FirstOrDefaultAsync(o => o.Id == orderId))!;
    }

    public async Task<ICollection<Order>> GetOrderByUserIdAsync(int userId)
    {
        var user = await _context.Users.Include(user => user.Orders).FirstOrDefaultAsync(u => u.Id == userId);
        return user!.Orders;
    }
}