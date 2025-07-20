namespace TravelApp.DataAccess.Models;

public class Review
{
    public int UserId { get; set; }
    public int AttractionId { get; set; }
    public decimal Rating { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    
    //Navigation properties
    public User User { get; set; } = null!;
    public Attraction Attraction { get; set; } = null!;
}