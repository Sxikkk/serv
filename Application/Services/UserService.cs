using Api.Middlewares;
using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services;

public class UserService: IUserService
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    public UserService(ITokenService tokenService, IUserRepository userRepository, IMapper mapper, IOrderRepository orderRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _mapper = mapper;
        _orderRepository = orderRepository;
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
            RefreshToken = null,
            ShoppingCart = new ShoppingCart()
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

    public async Task<User> ChangeUserAsync(ChangeUserRequestDto dto, int userId)
    {
        if (!await _userRepository.ExistsByIdAsync(userId)) 
            throw new Exception("Пользователь не найден");

        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) 
            throw new Exception("Пользователь не найден");

        var isUpdated = false;

        if (!string.IsNullOrEmpty(dto.OldPassword) && !string.IsNullOrEmpty(dto.NewPassword))
        {
            if (Hash.Password(dto.OldPassword) == user.PasswordHash)
                throw new InvalidOperationException("Неверный старый пароль");

            user.PasswordHash = Hash.Password(dto.NewPassword);
            isUpdated = true;
        }

        if (!string.IsNullOrEmpty(dto.NewFirstName) && dto.NewFirstName != user.FirstName)
        {
            user.FirstName = dto.NewFirstName;
            isUpdated = true;
        }

        if (!string.IsNullOrEmpty(dto.NewLastName) && dto.NewLastName != user.LastName)
        {
            user.LastName = dto.NewLastName;
            isUpdated = true;
        }

        if (isUpdated)
        {
            await _userRepository.UpdateAsync(user);
        }

        return user;
    }

    public async Task<List<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<List<UserResponseDto>>(users);
    }

    public async Task<UserResponseDto> DeleteUser(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user.RoleId == 1)
        {
            throw new Exception();
        }
        await _userRepository.DeleteAsync(userId);
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<ICollection<OrderResponseDto>> GetOrdersAsync()
    {
        var orders = await _orderRepository.GetOrdersAsync();
        var response = _mapper.Map<ICollection<OrderResponseDto>>(orders);
        return response;
    }
}