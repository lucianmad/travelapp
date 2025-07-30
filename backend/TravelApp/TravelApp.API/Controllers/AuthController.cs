using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.BusinessLogic.Dtos.Auth;
using TravelApp.BusinessLogic.Services.Abstractions;

namespace TravelApp.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        var userDto = await _userService.RegisterAsync(registerRequestDto);
        return CreatedAtAction(nameof(Register), new {id = userDto.Id}, userDto);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var authResponseDto = await _userService.LoginAsync(loginRequestDto);
        return Ok(authResponseDto);
    }
}