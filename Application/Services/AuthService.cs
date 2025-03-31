using Application.DTOs.Response;
using Application.Interfaces;
using Domain.DTOs;
using Infrastructure.Interfaces;
using Infrastructure.Repository;

namespace Application.Services;

public class AuthService: IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    private readonly ILogger<AuthService> _logger;
    
    public AuthService(ITokenService tokenService, IUserService userService, ILogger<AuthService> logger, IUserRepository userRepository, ITokenRepository tokenRepository)
    {
        _tokenService = tokenService;
        _userService = userService;
        _logger = logger;
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
    }
    
    public async Task<AuthResponseDto> RegistrationAsync(RegistrationRequestDto requestDto)
    {
        try
        {
            var existingUser = await _userRepository.GetByEmailAsync(requestDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Пользователь с таким email уже существует");
            }
            var newUser = await _userService.CreateUser(requestDto);
            _tokenRepository.AddToken(newUser.RefreshToken);
            
            var roleDto = new RoleRequestDto { Name = newUser.Role.Name };
            var accessToken = _tokenService.GetAccessToken(newUser, roleDto);
            var response = new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newUser.RefreshToken.Token,
                Email = newUser.Email
            };
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при регистрации пользователя");
            throw;
        }
    }

    public Task<AuthResponseDto> LoginAsync(LoginRequestDto requestDto)
    {
        throw new NotImplementedException();
    }

    public Task<string> RefreshTokenAsync(TokenRequestDto dto)
    {
        throw new NotImplementedException();
    }
}