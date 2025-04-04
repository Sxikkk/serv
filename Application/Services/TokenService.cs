using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Middlewares;
using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> jwtSetting)
    {
        _jwtSettings = jwtSetting.Value;
    }

    public SymmetricSecurityKey GetSymetricalKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

    public string GetRefreshToken(User user)
    {
        var refreshJwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            },
            expires: DateTime.UtcNow.AddDays(30), 
            signingCredentials: new SigningCredentials(GetSymetricalKey(), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(refreshJwt);
    }

    public string GetAccessToken(User user, AccessClaimsRequestDto dto)
    {
        ArgumentNullException.ThrowIfNull(user);

        var claims = new List<Claim>
        {
            new Claim("userId", user.Id.ToString()),
            new Claim("cartId", dto.CartId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, dto.RoleName)
        };
        var accessJwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(GetSymetricalKey(), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(accessJwt);
    }
}
