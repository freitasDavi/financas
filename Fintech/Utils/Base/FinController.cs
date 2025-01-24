using Fintech.DTOs.Responses;
using Fintech.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Utils.Base;

public class FinController : ControllerBase
{
    [NonAction]
    protected ActionResult HandleException(Exception exception)
    {
        switch (exception)
        {
            case BadRequestException badRequestException:
                return BadRequest(new BadRequestResponse
                {
                    Data = badRequestException.Data,
                    Details = badRequestException.Details,
                    Message = badRequestException.Message
                });
            case NotFoundException notFoundEx:
                return NotFound(new BadRequestResponse()
                {
                    Message = notFoundEx.Message
                });
            case UnauthorizedException ex:
                return Unauthorized(new BadRequestResponse()
                {
                    Message = ex.Message 
                });
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, new BadRequestResponse()
                {
                    Message = "An unexpected error occurred.",
                    Data = exception
                });
        }
    }
}