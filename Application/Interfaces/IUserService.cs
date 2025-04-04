using Application.DTOs.Response;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(RegistrationRequestDto dto);
    Task<User> ChangeUserAsync(ChangeUserRequestDto dto, int userId);
    Task<List<UserResponseDto>> GetAllUsersAsync();
    Task<UserResponseDto> DeleteUser(int userId);
    Task<ICollection<OrderResponseDto>> GetOrdersAsync();
}