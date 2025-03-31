using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public record RegistrationRequestDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; init; }
    public string Password { get; init; }
}