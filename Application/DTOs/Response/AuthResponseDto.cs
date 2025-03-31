namespace Application.DTOs.Response;

public record AuthResponseDto
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    public string Email { get; init; }
};