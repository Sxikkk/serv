using Api.Middlewares;
using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Services;

public class UserService: IUserService
{
    private readonly ITokenService _tokenService;

    public UserService(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    public async Task<User> CreateUser(RegistrationRequestDto dto)
    {
        var hashedPassword = Hash.Password(dto.Password);
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = hashedPassword,
            RoleId = 2,
            RefreshToken = null
        };
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = _tokenService.GetRefreshToken(user),
            ExpiresAt = DateTime.UtcNow.AddDays(30),
        };
        user.RefreshToken = refreshToken;

        return user;
    }
}