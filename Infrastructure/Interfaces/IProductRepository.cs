using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IProductRepository
{
    Task<ICollection<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int productId);
    Task AddProductAsync(Product product);
    Task ChangeQuantityAsync(int changeNumber, int productId);
    Task<Product> DeleteProductAsync(int productId);
}