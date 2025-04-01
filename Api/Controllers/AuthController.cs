using Application.DTOs.Response;
using Application.Interfaces;
using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("registration")]
    public async Task<IResult> Registration([FromBody] RegistrationRequestDto dto)
    {
        var response = await _authService.RegistrationAsync(dto);
        return Results.Ok(response);
    }

    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequestDto dto)
    {
        var response = await _authService.LoginAsync(dto);
        return Results.Ok(response);
    }

    [HttpPost("updateToken")]
    public async Task<IResult> UpdateToken(TokenRequestDto dto)
    {
        var response = await _authService.RefreshTokenAsync(dto);
        return Results.Ok(response);
    }
}