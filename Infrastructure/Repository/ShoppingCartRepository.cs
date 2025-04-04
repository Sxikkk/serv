using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ShoppingCartRepository: IShoppingCartRepository
{
    private readonly AppDbContext _context;

    public ShoppingCartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ShoppingCartItem> GetItemByIdAsync(int itemId, int userId)
    {
        var item = await _context.ShoppingCartItems
            .FirstOrDefaultAsync(it => it.Id == itemId && it.Cart.UserId == userId);
        return item;
    }
    
    public async Task<ICollection<ShoppingCartItem>> GetShoppingCartItemsAsync(int userId)
    {
        var cart = await GetShoppingCartAsync(userId);
        return cart.ShoppingCartItems;
    }

    public async Task<ShoppingCart> GetShoppingCartAsync(int userId)
    {
        var cart = await _context.ShoppingCarts
            .Include(ct => ct.ShoppingCartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);
        return cart;
    }

    public async Task DeleteItemFromCartAsync(int itemId, int userId)
    {
        var item = await GetItemByIdAsync(itemId, userId);
        _context.ShoppingCartItems.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task AddItemToCartAsync(ShoppingCartItem item)
    {
        await _context.ShoppingCartItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistItemByIdAsync(int itemId)
    {
        return await _context.ShoppingCartItems.AnyAsync(sct => sct.Id == itemId);
    }
}