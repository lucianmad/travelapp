using TravelApp.DataAccess.Models;

namespace TravelApp.BusinessLogic.DTOs.Country;

public class CountryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Flag { get; set; } = string.Empty;
    public IEnumerable<City> Cities { get; set; } = new List<City>();
}