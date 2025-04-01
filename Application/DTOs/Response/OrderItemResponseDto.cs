namespace Application.DTOs.Response;

public record OrderItemResponseDto
{
    public int Id { get; init; }
    public int ProductId { get; init; }
    public ProductResponseDto Product { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}
