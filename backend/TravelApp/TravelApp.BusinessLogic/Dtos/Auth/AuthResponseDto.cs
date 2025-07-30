using TravelApp.BusinessLogic.Dtos.User;

namespace TravelApp.BusinessLogic.Dtos.Auth;
public class AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public UserGetDto User { get; set; } = new();
}