using Contract.Abstractions.Shared;
using Contract.Services.V1.PetCenterService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class PetCenterServiceController : ApiController
{
    public PetCenterServiceController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.PetCenterServiceResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPetCenterServices([FromQuery] Query.GetPetCenterServiceQuery request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpGet("{PetCenterServiceId:int}")]
    [ProducesResponseType(typeof(Result<Responses.PetCenterServiceResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPetCenterServiceById([FromRoute] int PetCenterServiceId)
    {
        var result = await sender.Send(new Query.GetPetCenterServiceByIdQuery
        {
            Id = PetCenterServiceId
        });

        return Ok(result);
    }

    [HttpPut("{PetCenterServiceId:int}")]
    [ProducesResponseType(typeof(Result<Responses.PetCenterServiceResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePetCenterService([FromRoute] int PetCenterServiceId, Command.UpdatePetCenterServiceCommand request)
    {
        var updatePetCenterService = request with { Id = PetCenterServiceId };
        var result = await sender.Send(updatePetCenterService);
        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpDelete("{PetCenterServiceId:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePetCenterService([FromRoute] int PetCenterServiceId)
    {
        var result = await sender.Send(new Command.DeletePetCenterServiceCommand(PetCenterServiceId));
        return Ok(result);
    }
}
