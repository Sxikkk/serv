using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IAdminRepository
{
    Task<IList<User>> GetAllUsers();
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserById(int id);
    Task<IList<Product>> GetAllProducts();
    Task<Product> AddProduct();
    Task<Order> GetOrderById(int id);
}