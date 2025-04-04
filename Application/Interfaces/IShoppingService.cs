using Application.DTOs.Response;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IShoppingService
{
    Task<ICollection<ShoppingCartItemResponseDto>> GetShoppingCartItemsAsync(int userId);
    Task<ShoppingCartResponseDto> GetUserShoppingCartAsync(int userId);
    Task<ShoppingCartItemResponseDto> GetCartItemByIdAsync(int itemId, int userId);
    Task AddItemInCartAsync(ShoppingCartItemRequestDto dto);
    Task DeleteItemFromCartAsync(int itemId, int userId);
}