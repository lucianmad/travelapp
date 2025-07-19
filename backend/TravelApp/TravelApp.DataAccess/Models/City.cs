namespace TravelApp.DataAccess.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CountryId { get; set; }
    
    //Navigation properties
    public Country Country { get; set; } = null!;
    public IEnumerable<Attraction> Attractions { get; set; } = new List<Attraction>();
}