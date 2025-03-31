using Api.Middlewares;
using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services;

public class UserService: IUserService
{
    private readonly ITokenService _tokenService;
    private IUserRepository _userRepository;

    public UserService(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }
    
    public async Task<User> CreateUserAsync(RegistrationRequestDto dto)
    {
        var hashedPassword = Hash.Password(dto.Password);
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = hashedPassword,
            RoleId = 2,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            RefreshToken = null
        };
        await _userRepository.AddAsync(user);
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = _tokenService.GetRefreshToken(user),
            ExpiresAt = DateTime.UtcNow.AddDays(30),
        };
        user.RefreshToken = refreshToken;
        await _userRepository.UpdateAsync(user);
        
        return user;
    }

    public async Task<User> ChangeUserAsync(ChangeUserRequestDto dto, User user)
    {
        if (dto is { OldPassword: not null, NewPassword: not null })
        {
            var hashedOldDtoPassword = Hash.Password(dto.OldPassword);
            if (hashedOldDtoPassword == user.PasswordHash)
            {
                var newHashedPassword = Hash.Password(dto.NewPassword);
                user.PasswordHash = newHashedPassword;
                await _userRepository.UpdateAsync(user);
            }
        }

        if (dto.NewFirstName != null)
        {
            user.FirstName = dto.NewFirstName;
            await _userRepository.UpdateAsync(user);
        }

        if (dto.NewLastName != null)
        {
            user.LastName = dto.NewLastName;
            await _userRepository.UpdateAsync(user);
        }

        return user;
    }
}