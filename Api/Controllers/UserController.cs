using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class UserController
{
    [HttpGet("/dsa")]
    public string GetddsallUsers()
    {
        return "ЗАебись зареган юзер";
    }
}