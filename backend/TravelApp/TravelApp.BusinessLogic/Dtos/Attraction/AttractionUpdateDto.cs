namespace TravelApp.BusinessLogic.Dtos.Attraction;

public class AttractionUpdateDto
{
    public int CityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}