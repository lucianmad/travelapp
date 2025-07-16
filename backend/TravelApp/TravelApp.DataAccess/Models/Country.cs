namespace TravelApp.DataAccess.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Flag { get; set; } = string.Empty;
    
    //Navigation properties
    public IEnumerable<City> Cities { get; set; } = new List<City>();
}