namespace TravelApp.BusinessLogic.Dtos.City;

public class CityUpdateDto
{
    public int CountryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}