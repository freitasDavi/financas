using Fintech.DTOs.Requests;
using Fintech.DTOs.Responses;
using Fintech.Entities;
using Fintech.Enums;
using Fintech.Exceptions;
using Fintech.Interfaces;
using Fintech.Utils;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Services;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(ITokenService tokenService, DataContext context)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task Register(NewUserRequest request)
    {
        var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (userExists is not null)
            throw new UnauthorizedException(EnumAuthError.EmailAlreadyExists);
        
        if (request.Password != request.ConfirmPassword)
            throw new UnauthorizedException(EnumAuthError.LoginOrPassword);
        
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
            throw new UnauthorizedException(EnumAuthError.LoginOrPassword);
        
        if (!ComparePassword(request.Password, user.Password))
            throw new UnauthorizedException(EnumAuthError.LoginOrPassword);

        var token = _tokenService.GenerateToken(user);

        return token;
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