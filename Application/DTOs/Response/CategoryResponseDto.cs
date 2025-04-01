using System.Text.Json.Serialization;

namespace Application.DTOs.Response;

public record CategoryResponseDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
}
