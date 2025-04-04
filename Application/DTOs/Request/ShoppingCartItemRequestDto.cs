namespace Domain.DTOs;

public record ShoppingCartItemRequestDto
{
    public int CartId { get; init; }
    public int ProductId { get; init; }
    public int Quantity { get; init; }
};