using Application.Interfaces;
using Domain.DTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    private readonly IShoppingService _shoppingService;

    public UserController(IUserService userService, IShoppingService shoppingService)
    {
        _userService = userService;
        _shoppingService = shoppingService;
    }

    [HttpPut("{userId:int}")]
    public async Task<IResult> ChangeUserAsync(ChangeUserRequestDto dto, int userId)
    {
        var updatedUser = await _userService.ChangeUserAsync(dto, userId);
        return TypedResults.Ok(new { message = "Данные обновлены", user = updatedUser});
    }

    [HttpGet("{userId:int}/shoppingCart")]
    public async Task<IResult> GetShoppingCartAsync(int userId)
    {
        var cart = await _shoppingService.GetUserShoppingCartAsync(userId);
        return TypedResults.Ok(cart);
    }

    [HttpGet("{userId:int}/shoppingCart/Items")]
    public async Task<IResult> GetCartItemsAsync(int userId)
    {
        var items = await _shoppingService.GetShoppingCartItemsAsync(userId);
        return TypedResults.Ok(items);
    }
    
    [HttpGet("{userId:int}/shoppingCart/Items/{itemId:int}")]
    public async Task<IResult> GetCartItemsAsync(int userId, int itemId)
    {
        var item = await _shoppingService.GetCartItemByIdAsync(itemId, userId);
        return TypedResults.Ok(item);
    }

    [HttpPost("{userId:int}/shoppingCart/items")]
    public async Task<IResult> AddItemToCartAsync(int userId, ShoppingCartItemRequestDto dto)
    {
        await _shoppingService.AddItemInCartAsync(userId, dto);
        return TypedResults.Ok(new { message = "Товар добавлен в корзину" });
    }

    [HttpDelete("{userId}/shoppingCart/items")]
    public async Task<IResult> RemoveItemFromCartAsync(int userId, ShoppingCartItemRequestDto dto)
    {
        try
        {
            await _shoppingService.DeleteItemFromCartAsync(userId, dto);
            return TypedResults.Ok(new { message = "Товар удалён из корзины" });
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(new { message = ex.Message });
        }
    }
}