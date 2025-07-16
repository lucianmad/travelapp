using TravelApp.DataAccess.Enums;

namespace TravelApp.DataAccess.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; }
    
    //Navigation properties
    public IEnumerable<Review> Reviews { get; set; } = new List<Review>();
}