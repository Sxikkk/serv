using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(RegistrationRequestDto dto);
    Task<User> ChangeUserAsync(ChangeUserRequestDto dto, User user);
}