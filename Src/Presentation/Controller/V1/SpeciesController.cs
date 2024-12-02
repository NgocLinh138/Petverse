using Contract.Abstractions.Shared;
using Contract.Services.V1.Species;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class SpeciesController : ApiController
{
    public SpeciesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.SpeciesResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Speciess([FromQuery] Query.GetSpeciesQuery request)
    {
        var result = await sender.Send(
            new Query.GetSpeciesQuery
            {
                SearchTerm = request.SearchTerm,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            });
        return Ok(result);
    }

    [HttpGet("{SpeciesId}")]
    [ProducesResponseType(typeof(Result<Responses.SpeciesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Speciess([FromRoute] int SpeciesId)
    {
        var result = await sender.Send(new Query.GetSpeciesByIdQuery { Id = SpeciesId });
        return Ok(result);
    }

    //[Authorize(Roles = "admin, manager")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.SpeciesResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Speciess(Command.CreateSpeciesCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpPut("{SpeciesId:int}")]
    [ProducesResponseType(typeof(Result<Responses.SpeciesResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Speciess([FromRoute] int SpeciesId, Command.UpdateSpeciesCommand request)
    {
        var updateSpecies = request with { Id = SpeciesId };
        var result = await sender.Send(updateSpecies);
        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpDelete("{SpeciesId:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSpeciess([FromRoute] int SpeciesId)
    {
        var result = await sender.Send(new Command.DeleteSpeciesCommand(SpeciesId));
        return Ok(result);
    }

    // Breed
    [HttpGet("{SpeciesId:int}/Breed")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.BreedResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Breeds([FromRoute] int SpeciesId, string? SearchTerm, int PageIndex, int PageSize)
    {
        var result = await sender.Send(
            new Query.GetBreedQuery
            {
                SpeciesId = SpeciesId,
                SearchTerm = SearchTerm,
                PageIndex = PageIndex,
                PageSize = PageSize,
            });
        return Ok(result);
    }

    [HttpGet("{SpeciesId:int}/Breed/{BreedId:int}")]
    [ProducesResponseType(typeof(Result<Responses.BreedResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Breeds([FromRoute] int SpeciesId, [FromRoute] int BreedId)
    {
        var result = await sender.Send(new Query.GetBreedByIdQuery
        {
            Id = BreedId,
            SpeciesId = SpeciesId
        });
        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpPost("{SpeciesId}/Breed")]
    [ProducesResponseType(typeof(Result<Responses.BreedResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Breeds([FromRoute] int SpeciesId, Command.CreateBreedCommand request)
    {
        var createBreedrequest = request with { SpeciesId = SpeciesId };
        var result = await sender.Send(createBreedrequest);
        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpPut("{SpeciesId}/Breed/{BreedId:int}")]
    [ProducesResponseType(typeof(Result<Responses.BreedResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Breeds([FromRoute] int SpeciesId, [FromRoute] int BreedId, Command.UpdateBreedCommand request)
    {
        var updateBreed = request with { Id = BreedId, SpeciesId = SpeciesId };
        var result = await sender.Send(updateBreed);
        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpDelete("{SpeciesId}/Breed/{BreedId:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBreeds([FromRoute] int SpeciesId, [FromRoute] int BreedId)
    {
        var result = await sender.Send(new Command.DeleteBreedCommand(SpeciesId, BreedId));
        return Ok(result);
    }
}
