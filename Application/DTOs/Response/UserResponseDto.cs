using Domain.Entities;

namespace Application.DTOs.Response;

public record UserResponseDto
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public RoleResponseDto Role { get; init; }
    public ShoppingCartResponseDto ShoppingCart { get; init; }

    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}
