using Domain.DTOs;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.Interfaces;

public interface ITokenService
{
    SymmetricSecurityKey GetSymetricalKey();
    public string GetRefreshToken(User user);
    public string GetAccessToken(User user, RoleRequestDto roleRequestDto);
}