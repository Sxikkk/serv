using Application.DTOs.Response;
using Application.Interfaces;
using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("/registration")]
    public async Task<AuthResponseDto> Registration(RegistrationRequestDto dto)
    {
        var response = await _authService.RegistrationAsync(dto);
        return response;
    }
}