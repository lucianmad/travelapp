namespace TravelApp.BusinessLogic.Dtos.Attraction;

public class AttractionGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CityName { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public decimal AverageRating { get; set; }
}