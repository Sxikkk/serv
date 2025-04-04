namespace Domain.DTOs;

public record AccessClaimsRequestDto
{
    public string? RoleName { get; init; }
    public int CartId { get; init; }
};