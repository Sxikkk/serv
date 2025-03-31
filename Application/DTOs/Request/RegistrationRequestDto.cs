using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public record RegistrationRequestDto
{
    [Required(ErrorMessage = "Имя обязательно")]
    public string FirstName { get; init; }
    public string LastName { get; init; }
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный email")]   
    public string Email { get; init; }
    public string Password { get; init; }
}