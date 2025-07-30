using TravelApp.DataAccess.Models;

namespace TravelApp.BusinessLogic.Services.Abstractions;

public interface IJwtService
{
    string GenerateToken(User user);
    int ExpirationInHours { get; }
}