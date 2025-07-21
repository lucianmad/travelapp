using System.ComponentModel.DataAnnotations;

namespace TravelApp.BusinessLogic.Dtos.City;

public class CityCreateDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "CountryId must be positive")]
    public int CountryId { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]   
    public string Name { get; set; } = string.Empty;
    [StringLength(1000)]
    public string? Description { get; set; } = string.Empty;
}