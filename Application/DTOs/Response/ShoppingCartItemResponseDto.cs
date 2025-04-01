using Domain.Entities;

namespace Application.DTOs.Response;

public record ShoppingCartItemResponseDto
{
    public int Id { get; init; }
    public ProductResponseDto Product { get; init; }
    public int Quantity { get; init; }
}
