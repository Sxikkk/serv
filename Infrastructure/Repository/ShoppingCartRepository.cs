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
            .Include(pr => pr.Product)
            .ThenInclude(cat => cat.Category)
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
            .ThenInclude(pr => pr.Product)
            .ThenInclude(cat => cat.Category)
            .FirstOrDefaultAsync(c => c.UserId == userId);
        return cart;
    }

    public async Task DeleteItemFromCartAsync(int userId, int productId, int quantity)
    {
        var cart = await _context.ShoppingCarts
            .Include(c => c.ShoppingCartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null) throw new Exception("Корзина не найдена");

        var item = cart.ShoppingCartItems.FirstOrDefault(i => i.ProductId == productId);

        if (item == null) throw new Exception("Товар не найден в корзине");

        if (item.Quantity > quantity)
        {
            item.Quantity -= quantity; // Уменьшаем количество
        }
        else
        {
            _context.ShoppingCartItems.Remove(item); // Удаляем товар, если quantity <= 0
        }

        await _context.SaveChangesAsync();
    }

    public async Task AddItemToCartAsync(int userId, int productId, int quantity)
    {
        var cart = await _context.ShoppingCarts
            .Include(c => c.ShoppingCartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new ShoppingCart
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.ShoppingCarts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        var existingItem = cart.ShoppingCartItems.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.ShoppingCartItems.Add(new ShoppingCartItem
            {
                ProductId = productId,
                Quantity = quantity
            });
        }

        await _context.SaveChangesAsync();
    }


    public async Task<bool> ExistItemByIdAsync(int itemId)
    {
        return await _context.ShoppingCartItems.AnyAsync(sct => sct.Id == itemId);
    }
}