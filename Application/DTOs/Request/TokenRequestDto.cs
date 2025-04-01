using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public record TokenRequestDto
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; init; }
    [Required]
    public string RefreshToken { get; init; }
};