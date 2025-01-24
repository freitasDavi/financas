using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fintech.DTOs.DTO;
using Fintech.DTOs.Responses;
using Fintech.Entities;
using Fintech.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Fintech.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    
    public TokenService (IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public TokenResponse GenerateToken(User user)
    {
        var tokenSecret = Environment.GetEnvironmentVariable("TOKEN_SECRET");
        var tokenAudience = Environment.GetEnvironmentVariable("TOKEN_AUDIENCE");
        var tokenIssuer = Environment.GetEnvironmentVariable("TOKEN_ISSUER");

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name)
        };

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(tokenSecret ?? _configuration.GetSection("Token").GetValue<string>("Key")!)
        );

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expiresIn = DateTime.Now.AddDays(1);

        var tokenData = new JwtSecurityToken(
            claims: claims,
            issuer: tokenIssuer ?? _configuration.GetSection("Token").GetValue<string>("Issuer"),
            audience: tokenIssuer ?? _configuration.GetSection("Token").GetValue<string>("Audience"),
            expires: expiresIn,
            signingCredentials: credentials
        );

        var token =  new JwtSecurityTokenHandler().WriteToken(tokenData);
        
        return new TokenResponse { Token = token, Expires = expiresIn };
    }

    public TokenDTO DecodeTokenData(string stringfiedToken)
    {
        var tokenDecoded = new JwtSecurityTokenHandler().ReadJwtToken(stringfiedToken);
        
        var id = tokenDecoded.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);
        var name = tokenDecoded.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        var email = tokenDecoded.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

        return new TokenDTO
        {
            Id = (long)Convert.ToDouble(id.Value),
            Name = name.Value,
            Email = email.Value
        };
    }
}