using Fintech.DTOs.Requests;
using Fintech.DTOs.Responses;
using Fintech.Interfaces;
using Fintech.Utils.Base;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : FinController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(Exception))]
    public async Task<IActionResult> Register([FromBody] NewUserRequest request)
    {
        try
        {
            await _authService.Register(request);

            return Created();
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(type: typeof(TokenResponse), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _authService.Login(request);
            
            return Ok(token);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
}