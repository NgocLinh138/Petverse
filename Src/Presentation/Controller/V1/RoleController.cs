using Contract.Abstractions.Shared;
using Contract.Services.V1.Role;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

//[Authorize(Roles = "admin")]
public class RoleController : ApiController
{
    public RoleController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.RoleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Roles()
    {
        var result = await sender.Send(
            new Query.GetRoleQuery
            {
                SearchTerm = null,
                SortColumn = null,
                SortOrder = null,
                PageIndex = 1,
                PageSize = 10
            });
        return Ok(result);
    }

    [HttpGet("{RoleId}")]
    [ProducesResponseType(typeof(Result<Responses.RoleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Roles([FromRoute] Guid RoleId)
    {
        var result = await sender.Send(new Query.GetRoleByIdQuery { Id = RoleId });
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.RoleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Roles(Command.CreateRoleCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [HttpPut("{RoleId:Guid}")]
    [ProducesResponseType(typeof(Result<Responses.RoleResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Roles([FromRoute] Guid RoleId, Command.UpdateRoleCommand request)
    {
        var updateRole = request with { Id = RoleId };
        var result = await sender.Send(updateRole);
        return Ok(result);
    }


    [HttpDelete("{RoleId:Guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRoles([FromRoute] Guid RoleId)
    {
        var result = await sender.Send(new Command.DeleteRoleCommand(RoleId));
        return Ok(result);
    }
}
