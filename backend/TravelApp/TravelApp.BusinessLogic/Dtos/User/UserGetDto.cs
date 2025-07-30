namespace TravelApp.BusinessLogic.Dtos.User;

public class UserGetDto
{ 
    public int Id { get; set; } 
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}