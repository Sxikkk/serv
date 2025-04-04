using Application.DTOs.Response;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IShoppingService
{
    Task<ICollection<ShoppingCartItemResponseDto>> GetShoppingCartItemsAsync(int userId);
    Task<ShoppingCartResponseDto> GetUserShoppingCartAsync(int userId);
    Task<int> GetCartIdByUserIdAsync(int userId);
    Task<ShoppingCartItemResponseDto> GetCartItemByIdAsync(int itemId, int userId);
    Task AddItemInCartAsync(int userId, ShoppingCartItemRequestDto dto);
    Task DeleteItemFromCartAsync(int userId, ShoppingCartItemRequestDto dto);
    Task<decimal> CalculatePriceAsync(int userid);
}