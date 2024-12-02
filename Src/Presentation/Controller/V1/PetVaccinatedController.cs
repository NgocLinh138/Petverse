using Contract.Abstractions.Shared;
using Contract.Services.V1.PetVaccinated;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class PetVaccinatedController : ApiController
{
    public PetVaccinatedController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.PetVaccinatedResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PetSitters([FromQuery] Query.GetPetVaccinatedQuery request)
    {
        var result = await sender.Send(
            new Query.GetPetVaccinatedQuery
            {
                PetId = request.PetId,
                SearchTerm = request.SearchTerm,
                SortColumn = request.SortColumn,
                SortOrder = request.SortOrder,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            });
        return Ok(result);
    }

    [HttpGet("{PetVaccinatedId:int}")]
    [ProducesResponseType(typeof(Result<Responses.PetVaccinatedResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPetById([FromRoute] int PetVaccinatedId)
    {
        var result = await sender.Send(new Query.GetPetVaccinatedByIdQuery
        {
            Id = PetVaccinatedId
        });

        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.PetVaccinatedResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePet([FromForm] Command.CreatePetVaccinatedCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPut("{PetVaccinatedId:int}")]
    [ProducesResponseType(typeof(Result<Responses.PetVaccinatedResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePet([FromRoute] int PetVaccinatedId, [FromForm] Command.UpdatePetVaccinatedCommand request)
    {
        var updatePet = new Command.UpdatePetVaccinatedCommand
        {
            Id = PetVaccinatedId,
            Name = request.Name,
            Image = request.Image,
            DateVaccinated = request.DateVaccinated
        };

        var result = await sender.Send(updatePet);

        return Ok(result);
    }

    [HttpDelete("{PetVaccinatedId:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePet([FromRoute] int PetVaccinatedId)
    {
        var result = await sender.Send(new Command.DeletePetVaccinatedCommand
        {
            Id = PetVaccinatedId
        });

        return Ok(result);
    }
}
