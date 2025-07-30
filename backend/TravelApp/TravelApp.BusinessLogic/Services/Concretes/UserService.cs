using Microsoft.AspNetCore.Identity;
using TravelApp.BusinessLogic.Dtos.Auth;
using TravelApp.BusinessLogic.Dtos.User;
using TravelApp.BusinessLogic.Exceptions;
using TravelApp.BusinessLogic.Services.Abstractions;
using TravelApp.DataAccess.Enums;
using TravelApp.DataAccess.Models;
using TravelApp.DataAccess.Repositories.Abstractions;

namespace TravelApp.BusinessLogic.Services.Concretes;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepository, IJwtService jwtService, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<UserGetDto> RegisterAsync(RegisterRequestDto registerRequestDto)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(registerRequestDto.Username) ??
                           await _userRepository.GetByEmailAsync(registerRequestDto.Email);

        if (existingUser != null)
        {
            throw new UserAlreadyExistsException();
        }

        var user = new User
        {
            Email = registerRequestDto.Email,
            Username = registerRequestDto.Username,
            Role = Role.User
        };
        
        user.Password = _passwordHasher.HashPassword(user, registerRequestDto.Password);
        await _userRepository.CreateAsync(user);

        return new UserGetDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginRequestDto.Email);

        if (user == null)
        {
            throw new InvalidCredentialsException();
        }
        
        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, loginRequestDto.Password);
        if (verificationResult != PasswordVerificationResult.Success)
        {
            throw new InvalidCredentialsException();
        }
        
        var accessToken = _jwtService.GenerateToken(user);
        
        return new AuthResponseDto
        {
            AccessToken = accessToken,
            Expiration = DateTime.UtcNow.AddHours(_jwtService.ExpirationInHours),
            User = new UserGetDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role.ToString()
            }
        };
    }
}