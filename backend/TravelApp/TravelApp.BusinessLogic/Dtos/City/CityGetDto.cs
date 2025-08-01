using TravelApp.DataAccess.Models;

namespace TravelApp.BusinessLogic.Dtos.City;

public class CityGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public IEnumerable<string> AttractionsNames { get; set; } = new List<string>();
    public string CountryName { get; set; } = string.Empty;
}