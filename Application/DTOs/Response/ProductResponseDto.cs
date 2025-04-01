using System.Text.Json.Serialization;

namespace Application.DTOs.Response;

public record ProductResponseDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public CategoryResponseDto Category { get; init; }
    [JsonIgnore]
    public ICollection<ReviewResponseDto> Reviews { get; init; } = new List<ReviewResponseDto>();
}
