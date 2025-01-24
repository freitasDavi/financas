using Fintech.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace Fintech.Middlewares;

public class TokenAuthenticationMiddleware : IMiddleware
{
    private readonly ITokenService _tokenService;

    public TokenAuthenticationMiddleware(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var userToken = await context.GetTokenAsync("access_token");

            var tokenData = _tokenService.DecodeTokenData(userToken);
            
            context.Items["UserToken"] = tokenData;
        }
        
        await next(context);
    }
}