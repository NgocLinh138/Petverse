using Contract.Abstractions.Shared;
using Contract.Services.V1.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class ApplicationController : ApiController
{
    public ApplicationController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ApplicationResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetApplications([FromQuery] Query.GetApplicationQuery request)
    {
        var result = await sender.Send(
             new Query.GetApplicationQuery
             {
                 UserId = request.UserId,
                 Status = request.Status,
                 SearchTerm = request.SearchTerm,
                 SortColumn = request.SortColumn,
                 SortOrder = request.SortOrder,
                 PageIndex = request.PageIndex,
                 PageSize = request.PageSize,
             });

        return Ok(result);
    }

    [HttpGet("{ApplicationId:int}")]
    [ProducesResponseType(typeof(Result<Responses.ApplicationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetApplicationById([FromRoute] int ApplicationId)
    {
        var result = await sender.Send(new Query.GetApplicationByIdQuery
        {
            Id = ApplicationId
        });

        return Ok(result);
    }


    [HttpGet("Status/{UserId:Guid}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ApplicationResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetApplicationByUserId([FromRoute] Guid UserId)
    {
        var result = await sender.Send(new Query.GetApplicationStatusByUserIdQuery
        {
            UserId = UserId
        });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.ApplicationResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateApplication([FromForm] Command.CreateApplicationCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [Authorize(Roles = "admin, manager")]
    [HttpPut("{ApplicationId:int}")]
    [ProducesResponseType(typeof(Result<Responses.ApplicationResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateApplication([FromRoute] int ApplicationId, Command.UpdateApplicationStatusCommand request)
    {
        var updateBreed = new Command.UpdateApplicationStatusCommand
        {
            Id = ApplicationId,
            RoleId = request.RoleId,
            Status = request.Status,
            CancelReason = request.CancelReason,
            IsVerified = request.IsVerified
        };

        var result = await sender.Send(updateBreed);

        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpDelete("{ApplicationId:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteApplication([FromRoute] int ApplicationId)
    {
        var result = await sender.Send(new Command.DeleteApplicationCommand
        {
            Id = ApplicationId
        });
        return Ok(result);
    }
}
