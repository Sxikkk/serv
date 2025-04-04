using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProductRepository: IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Reviews)
            .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
         return (await _context.Products
             .Include(p => p.Category)
             .Include(p => p.Reviews)
             .FirstOrDefaultAsync(p => p.Id == productId))!;
    }

    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeQuantityAsync(int cn, int prodId)
    {
        var product = await GetProductByIdAsync(prodId);
        product.Quantity += cn;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int prodId)
    {
        var prod = await GetProductByIdAsync(prodId);
        _context.Products.Remove(prod);
        await _context.SaveChangesAsync();
    }
}