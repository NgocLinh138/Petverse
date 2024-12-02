using Contract.Abstractions.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Abstractions;

[Authorize]
[ApiController]
[Route("Api/V{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender sender;

    protected ApiController(ISender sender) => this.sender = sender;

    protected IActionResult HandlerFailure(Result result)
        => result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(
                    CreateProblemDetails(
                        "Validation Error",
                        StatusCodes.Status400BadRequest,
                        result.Message,
                        validationResult.Errors)),
            _ => BadRequest(result)
        };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        string message,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Detail = message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}

