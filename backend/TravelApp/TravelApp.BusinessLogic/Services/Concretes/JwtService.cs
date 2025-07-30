using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TravelApp.BusinessLogic.Services.Abstractions;
using TravelApp.DataAccess.Models;

namespace TravelApp.BusinessLogic.Services.Concretes;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expirationInHours;
    
    public int ExpirationInHours => _expirationInHours;
    
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
        _secretKey = _configuration["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("Secret key not found in configuration");
        _issuer = _configuration["JwtSettings:Issuer"] ?? throw new InvalidOperationException("Issuer not found in configuration");
        _audience = _configuration["JwtSettings:Audience"] ?? throw new InvalidOperationException("Audience not found in configuration");
        _expirationInHours = int.Parse(_configuration["JwtSettings:ExpirationInHours"] 
                                       ?? throw new InvalidOperationException("Expiration in hours not found in configuration"));
    }
    
    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claim = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("userId", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claim,
            expires: DateTime.UtcNow.AddHours(_expirationInHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}