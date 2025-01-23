using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fintech.DTOs.Requests;
using Fintech.DTOs.Responses;
using Fintech.Entities;
using Fintech.Interfaces;
using Fintech.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Fintech.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;

    public AuthService(IConfiguration configuration, DataContext context)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task Register(NewUserRequest request)
    {
        var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (userExists is not null)
            throw new BadHttpRequestException("User already exists");
        
        if (request.Password != request.ConfirmPassword)
            throw new BadHttpRequestException("Passwords do not match");
        
        var hashedPassword = HashPassword(request.Password);
        
        var newUser = new User
        {
            Email = request.Email,
            Name = request.Name,
            Active = true,
            Endereco = request.Endereco,
            Password = hashedPassword,
            Premium = false,
        };
        
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
    }

    public async Task<TokenResponse> Login(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user is null)
            throw new BadHttpRequestException("Invalid username or password!");
        
        if (!ComparePassword(request.Password, user.Password))
            throw new BadHttpRequestException("Invalid username or password!");

        var token = GenerateToken(user);

        return token;
    }
    
    private TokenResponse GenerateToken(User user)
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

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool ComparePassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}