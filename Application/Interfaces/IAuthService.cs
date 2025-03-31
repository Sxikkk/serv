using Application.DTOs.Response;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegistrationAsync(RegistrationRequestDto requestDto);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto requestDto);
    Task<string> RefreshTokenAsync(TokenRequestDto dto);
}