using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services;

public class ShoppingService: IShoppingService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public ShoppingService(IShoppingCartRepository shoppingCartRepository, IUserRepository userRepository, IMapper mapper)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<ICollection<ShoppingCartItemResponseDto>> GetShoppingCartItemsAsync(int userId)
    {
        if (!await _userRepository.ExistsByIdAsync(userId)) throw new Exception("Пользователь не найден");

        var cartItems = await _shoppingCartRepository.GetShoppingCartItemsAsync(userId);
        var mappedItems = _mapper.Map<ICollection<ShoppingCartItemResponseDto>>(cartItems);
        return mappedItems;
    }

    public async Task<ShoppingCartResponseDto> GetUserShoppingCartAsync(int userId)
    {
        if (!await _userRepository.ExistsByIdAsync(userId)) throw new Exception("Пользователь не найден");

        var cart = await _shoppingCartRepository.GetShoppingCartAsync(userId);
        var mappedCart = _mapper.Map<ShoppingCartResponseDto>(cart);
        return mappedCart;
    }

    public async Task<int> GetCartIdByUserIdAsync(int userId)
    {
        if (!await _userRepository.ExistsByIdAsync(userId)) throw new Exception("Пользователь не найден");

        var cart = await _shoppingCartRepository.GetShoppingCartAsync(userId);
        return cart.Id;
    }

    public async Task<ShoppingCartItemResponseDto> GetCartItemByIdAsync(int itemId, int userId)
    {
        if (!await _shoppingCartRepository.ExistItemByIdAsync(itemId)) throw new Exception("Товар не найден");
        if (!await _userRepository.ExistsByIdAsync(userId)) throw new Exception("Пользователь не найден");

        var item = await _shoppingCartRepository.GetItemByIdAsync(itemId, userId);
        var mappedItem = _mapper.Map<ShoppingCartItemResponseDto>(item);
        return mappedItem;
    }
    
    public async Task AddItemInCartAsync(int userId, ShoppingCartItemRequestDto dto)
    {
        await _shoppingCartRepository.AddItemToCartAsync(userId, dto.ProductId, dto.Quantity);
    }

    public async Task DeleteItemFromCartAsync(int userId, ShoppingCartItemRequestDto dto)
    {
        await _shoppingCartRepository.DeleteItemFromCartAsync(userId, dto.ProductId, dto.Quantity);

    }

    public async Task<decimal> CalculatePriceAsync(int userId)
    {
        var cart = await GetShoppingCartItemsAsync(userId);
        return cart.Sum(item => item.Product.Price);
    }
}