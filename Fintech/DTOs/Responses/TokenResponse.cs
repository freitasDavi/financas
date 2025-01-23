namespace Fintech.DTOs.Responses;

public class TokenResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
}