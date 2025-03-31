using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<User> CreateUser(RegistrationRequestDto dto);
}