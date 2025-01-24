using Fintech.Enums;

namespace Fintech.Exceptions;

public class UnauthorizedException(EnumAuthError errorType) : Exception
{
    public new string Message { get; set; } = errorType switch
    {
        EnumAuthError.EmailAlreadyExists => "Email already exists.",
        EnumAuthError.LoginOrPassword => "Login or Password incorrect.",
        _ => "Unknown Error."
    };
}