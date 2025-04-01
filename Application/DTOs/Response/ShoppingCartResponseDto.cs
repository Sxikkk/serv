using Domain.Entities;

namespace Application.DTOs.Response;

public record ShoppingCartResponseDto
{
    public int UserId { get; init; }
    public ICollection<ShoppingCartItemResponseDto> ShoppingCartItems { get; init; } = new List<ShoppingCartItemResponseDto>();
    public DateTime CreatedAt { get; init; }
}
