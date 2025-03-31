using Api.Middlewares;
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
    private readonly IRoleRepository _roleRepository;
    private readonly IUserService _userService;
    private readonly ILogger<AuthService> _logger;
    
    public AuthService(
        ITokenService tokenService, 
        IUserService userService, 
        ILogger<AuthService> logger, 
        IUserRepository userRepository, 
        ITokenRepository tokenRepository,
        IRoleRepository roleRepository
        )
    {
        _tokenService = tokenService;
        _userService = userService;
        _roleRepository = roleRepository;
        _logger = logger;
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
    }
    
    public async Task<AuthResponseDto> RegistrationAsync(RegistrationRequestDto requestDto)
    {
        try
        {
            if (await _userRepository.ExistsByEmailAsync(requestDto.Email))
            {
                throw new Exception($"Пользователь с email {requestDto.Email} уже существует");
            }

            var newUser = await _userService.CreateUserAsync(requestDto);
            await _tokenRepository.AddTokenAsync(newUser.RefreshToken);
        
            // Получаем название роли (с обработкой возможных ошибок)
            var roleName = await _roleRepository.GetRoleNameByIdAsync(newUser.RoleId);
        
            var accessToken = _tokenService.GetAccessToken(newUser, new RoleRequestDto { 
                Name = roleName 
            });

            _logger.LogInformation("Успешная регистрация: {Email}", newUser.Email);
            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newUser.RefreshToken?.Token ?? throw new InvalidOperationException("Refresh token не создан"),
                Email = newUser.Email
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка регистрации для {Email}", requestDto.Email);
            throw;
        }
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto requestDto)
{
    if (requestDto == null)
        throw new ArgumentNullException(nameof(requestDto));

    try
    {
        var user = await _userRepository.GetByEmailAsync(requestDto.Email);
        if (user == null)
        {
            _logger.LogWarning("Попытка входа с несуществующим email: {Email}", requestDto.Email);
            throw new UnauthorizedAccessException("Неверный email или пароль");
        }

        var hashedPassword = Hash.Password(requestDto.Password);
        if (hashedPassword != user.PasswordHash)
        {
            _logger.LogWarning("Неверный пароль для пользователя {Email}", requestDto.Email);
            throw new UnauthorizedAccessException("Неверный email или пароль");
        }

        var roleName = await _roleRepository.GetRoleNameByIdAsync(user.RoleId);
        if (string.IsNullOrEmpty(roleName))
        {
            _logger.LogError("Роль не найдена для пользователя {UserId}", user.Id);
            throw new InvalidOperationException("Роль пользователя не определена");
        }

        var newRefreshToken = _tokenService.GetRefreshToken(user);
        var newAccessToken = _tokenService.GetAccessToken(user, new RoleRequestDto { Name = roleName });

        await _tokenRepository.UpdateTokenAsync(user.Id, newRefreshToken);

        _logger.LogInformation("Успешный вход пользователя {Email}", user.Email);
        
        return new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            Email = user.Email
        };
    }
    catch (Exception e) when (e is not UnauthorizedAccessException and not InvalidOperationException)
    {
        _logger.LogError(e, "Ошибка входа для {Email}", requestDto.Email);
        throw new UnauthorizedAccessException("Ошибка аутентификации");
    }
}

    public Task<string> RefreshTokenAsync(TokenRequestDto dto)
    {
        throw new NotImplementedException();
    }
}