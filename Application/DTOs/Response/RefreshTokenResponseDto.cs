namespace Application.DTOs.Response;

public record RefreshTokenResponseDto
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public string Token { get; init; }
    public DateTime ExpiresAt { get; init; }
}
