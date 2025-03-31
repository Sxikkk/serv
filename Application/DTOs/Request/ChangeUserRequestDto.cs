namespace Domain.DTOs;

public record ChangeUserRequestDto
{
    public string? NewFirstName { get; init; } = null;
    public string? NewLastName { get; init; } = null;
    public string? NewPassword { get; init; } = null;
    public string? OldPassword { get; init; } = null;
}