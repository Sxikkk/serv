using System.Text.Json.Serialization;

namespace Application.DTOs.Response;

public record ReviewResponseDto
{
    public int Id { get; init; }
    public int UserId { get; init; }
    [JsonIgnore]
    public UserResponseDto User { get; init; }
    public int ProductId { get; init; }
    public ProductResponseDto Product { get; init; }
    public int Rating { get; init; }
    public string Comment { get; init; }
    public DateTime CreatedAt { get; init; }
}
