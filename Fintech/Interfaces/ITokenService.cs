using Fintech.DTOs.DTO;
using Fintech.DTOs.Responses;
using Fintech.Entities;

namespace Fintech.Interfaces;

public interface ITokenService
{
    TokenResponse GenerateToken(User user);
    TokenDTO DecodeTokenData (string token);
}