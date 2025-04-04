using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController: ControllerBase
{
    private readonly IUserService _userService;

    public AdminController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("users")]
    public async Task<IResult> GetAllUsersMinimal()
    {
        var users = await _userService.GetAllUsersAsync();
        return TypedResults.Ok(users);
    }

    [HttpDelete("deleteUser")]
    public async Task<IResult> DeleteUserById(int userId)
    {
        var deleteUser = await _userService.DeleteUser(userId);
        return TypedResults.Ok(deleteUser);
    }

    [HttpGet("orders")]
    public async Task<IResult> GetAllOrders()
    {
        var response = await _userService.GetOrdersAsync();
        return TypedResults.Ok(response);
    }
}