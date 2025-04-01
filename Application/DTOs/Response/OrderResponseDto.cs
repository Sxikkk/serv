using Domain.Enums;

namespace Application.DTOs.Response;

public record OrderResponseDto
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public Status Status { get; init; }
    public decimal TotalPrice { get; init; }
    public ICollection<OrderItemResponseDto> OrderItems { get; init; } = new List<OrderItemResponseDto>();
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}
