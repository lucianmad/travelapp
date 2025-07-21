using System.ComponentModel.DataAnnotations;

namespace TravelApp.BusinessLogic.DTOs.Country;

public class CountryUpdateDto
{
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)] 
    public string? Flag { get; set; }
}