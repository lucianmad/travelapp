using TravelApp.BusinessLogic.Dtos.Auth;
using TravelApp.BusinessLogic.Dtos.User;

namespace TravelApp.BusinessLogic.Services.Abstractions;

public interface IUserService
{
    Task<UserGetDto> RegisterAsync(RegisterRequestDto registerRequestDto);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
}