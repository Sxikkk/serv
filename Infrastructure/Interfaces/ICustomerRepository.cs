using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ICustomerRepository: IUserRepository
{
    Task<IList<Product>> GetAllProducts();
    Task<Order> GetOrderById(int id);
}