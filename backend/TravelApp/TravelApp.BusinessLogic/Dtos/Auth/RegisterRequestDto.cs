using System.ComponentModel.DataAnnotations;
using TravelApp.DataAccess.Enums;

namespace TravelApp.BusinessLogic.Dtos.Auth;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Username is required")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters")]
    public string Username { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password confirmation is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}