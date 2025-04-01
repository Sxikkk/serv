using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IOrderRepository
{
    Task<ICollection<Order>> GetOrdersAsync();
    Task<Order> GetOrderByOrderIdAsync(int orderId);
    Task<ICollection<Order>> GetOrderByUserIdAsync( int userId);
}