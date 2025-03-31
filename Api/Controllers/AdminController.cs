using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController
{
    [HttpGet("/allUsers")]
    public string GetAllUsers()
    {
        return "ЗАебись зареган админ";
    }
}