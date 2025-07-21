namespace TravelApp.DataAccess.Models;

public class Attraction
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CityId { get; set; }
    
    //Navigation properties
    public City City { get; set; } = null!;
    public IEnumerable<Review> Reviews { get; set; } = new List<Review>();
}