using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IShoppingCartRepository
{
    Task<ICollection<ShoppingCartItem>> GetShoppingCartItemsAsync(int userId);
    Task<ShoppingCartItem> GetItemByIdAsync(int itemId, int userId);
    Task<ShoppingCart> GetShoppingCartAsync(int userId);
    Task DeleteItemFromCartAsync(int userId, int productId, int quantity);
    Task AddItemToCartAsync(int userId, int productId, int quantity);
    Task<bool> ExistItemByIdAsync(int itemId);
}