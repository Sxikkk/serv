using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public record TokenRequestDto (
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    string Email,
    [Required]
    string RefreshToken 
);