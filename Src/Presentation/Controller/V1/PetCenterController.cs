using Contract.Abstractions.Shared;
using Contract.Services.V1.PetCenter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class PetCenterController : ApiController
{
    public PetCenterController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.PetCenterGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PetCenters([FromQuery] Query.GetPetCenterQuery request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpGet("{PetCenterId}")]
    [ProducesResponseType(typeof(Result<Responses.PetCenterGetByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PetCenters([FromRoute] Guid PetCenterId)
    {
        var result = await sender.Send(new Query.GetPetCenterByIdQuery(PetCenterId));
        return Ok(result);
    }

    [HttpGet("Top5PetCenter")]
    [ProducesResponseType(typeof(Result<Responses.TopPetCenterData>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Top5PetCenterLastMonth([FromQuery] Query.GetTop5PetCenterQuery request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPut("{PetCenterId:Guid}")]
    [ProducesResponseType(typeof(Result<Responses.PetCenterResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PetCenters([FromRoute] Guid PetCenterId, Command.UpdatePetCenterCommand request)
    {
        var updatePetCenter = request with { Id = PetCenterId };
        var result = await sender.Send(updatePetCenter);
        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpDelete("{PetCenterId:Guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePetCenters([FromRoute] Guid PetCenterId)
    {
        var result = await sender.Send(new Command.DeletePetCenterCommand(PetCenterId));
        return Ok(result);
    }
}
