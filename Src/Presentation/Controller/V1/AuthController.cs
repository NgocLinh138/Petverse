using Asp.Versioning;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

[AllowAnonymous]
[ApiVersion("1.0")]
public class AuthController : ApiController
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(Result<Responses.UserAuthenticatedResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LoginAsync(Query.LoginQuery request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpGet("RefreshToken")]
    [ProducesResponseType(typeof(Result<Responses.UserAuthenticatedResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken(Query.TokenQuery request)
    {
        var result = await sender.Send(request);

        if (!result.IsSuccess)
            HandlerFailure(result);

        return Ok(result);
    }

    [HttpPost("ForgetPassword")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> ForgetPassword(Command.ForgetPasswordCommand request)
    {
        var result = await sender.Send(request);

        if (!result.IsSuccess)
            HandlerFailure(result);

        return Ok(result);
    }

}
