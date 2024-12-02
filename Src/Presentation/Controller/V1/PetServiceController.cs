using Contract.Abstractions.Shared;
using Contract.Services.V1.PetService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class PetServiceController : ApiController
{
    public PetServiceController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.PetServiceResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPetServices([FromQuery] Query.GetPetServiceQuery request)
    {
        var result = await sender.Send(request);

        return Ok(result);
    }

    [HttpGet("{PetServiceId:int}")]
    [ProducesResponseType(typeof(Result<Responses.PetServiceResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPetServiceById([FromRoute] int PetServiceId)
    {
        var result = await sender.Send(new Query.GetPetServiceByIdQuery
        {
            Id = PetServiceId
        });

        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.PetServiceResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePetService([FromBody] Command.CreatePetServiceCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpPut("{PetServiceId:int}")]
    [ProducesResponseType(typeof(Result<Responses.PetServiceResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePetService([FromRoute] int PetServiceId, Command.UpdatePetServiceCommand request)
    {
        var updateBreed = new Command.UpdatePetServiceCommand
        {
            Id = PetServiceId,
            Name = request.Name,
            Description = request.Description
        };

        var result = await sender.Send(updateBreed);

        return Ok(result);
    }


    [Authorize(Roles = "admin, manager")]
    [HttpDelete("{PetServiceId:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePetService([FromRoute] int PetServiceId)
    {
        var result = await sender.Send(new Command.DeletePetServiceCommand
        {
            Id = PetServiceId
        });

        return Ok(result);
    }
}
