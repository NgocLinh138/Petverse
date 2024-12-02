using Contract.Abstractions.Shared;
using Contract.Services.V1.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class UserController : ApiController
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.UserGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Users([FromQuery] Query.GetUserQuery request)
    {
        var result = await sender.Send(
            new Query.GetUserQuery
            {
                RoleId = request.RoleId,
                SearchTerm = request.SearchTerm,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
        return Ok(result);
    }

    [HttpGet("{UserId:Guid}")]
    [ProducesResponseType(typeof(Result<Responses.UserGetByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Users([FromRoute] Guid UserId)
    {
        var result = await sender.Send(new Query.GetUserByIdQuery(UserId));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.UserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Users(Command.CreateUserCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPut("{UserId:Guid}")]
    [ProducesResponseType(typeof(Result<Responses.UserResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Users([FromRoute] Guid UserId, [FromForm] Command.UpdateUserCommand request)
    {
        var updateUser = request with { Id = UserId };
        var result = await sender.Send(updateUser);
        return Ok(result);
    }

    //[HttpPut("{UserId:Guid}/AssignRole/{roleId:Guid}")]
    //[ProducesResponseType(typeof(Result<Responses.UserResponse>), StatusCodes.Status202Accepted)]
    //[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> Users([FromRoute] Guid UserId, [FromRoute] Guid roleId)
    //{
    //    var updateUser = new Command.AssignRoleCommand
    //    {
    //        UserId = UserId,
    //        RoleId = roleId,
    //    };
    //    var result = await sender.Send(updateUser);
    //    return Ok(result);
    //}

    //[HttpPut("{userId}/ChangePassword")]
    //[RemoveCache(cacheKey)]
    //[ProducesResponseType(typeof(Result<Responses.UserResponse>), StatusCodes.Status202Accepted)]
    //[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> ChangePassword([FromRoute] int userId, Command.ChangePasswordCommand request)
    //{
    //    var updateUser = new Command.ChangePasswordCommand
    //    (
    //        userId,
    //        request.OldPassword,
    //        request.NewPassword
    //    );
    //    var result = await sender.Send(updateUser);
    //    return Ok(result);
    //}


    [HttpDelete("{UserId:Guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUsers([FromRoute] Guid UserId)
    {
        var result = await sender.Send(new Command.DeleteUserCommand(UserId));
        return Ok(result);
    }
}
