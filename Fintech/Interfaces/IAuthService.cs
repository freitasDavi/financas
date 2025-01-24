using System.IdentityModel.Tokens.Jwt;
using Fintech.DTOs.Requests;
using Fintech.DTOs.Responses;

namespace Fintech.Interfaces;

public interface IAuthService
{
    Task Register(NewUserRequest request);
    Task<TokenResponse> Login(LoginRequest request);
}