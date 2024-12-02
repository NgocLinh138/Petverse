using Contract.Abstractions.Shared;
using Contract.Services.V1.Vaccine;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class VaccineController : ApiController
{
    public VaccineController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.VaccineResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetVaccines([FromQuery] Query.GetVaccineQuery request)
    {
        var result = await sender.Send(
            new Query.GetVaccineQuery
            {
                SpeciesId = request.SpeciesId,
                SearchTerm = request.SearchTerm,
                SortColumn = request.SortColumn,
                SortOrder = request.SortOrder,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            });
        return Ok(result);
    }

    [HttpGet("{VaccineId:int}")]
    [ProducesResponseType(typeof(Result<Responses.VaccineResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetVaccineById([FromRoute] int VaccineId)
    {
        var result = await sender.Send(new Query.GetVaccineByIdQuery
        {
            Id = VaccineId
        });

        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.VaccineResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateVaccine([FromBody] Command.CreateVaccineCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpPut("{VaccineId:int}")]
    [ProducesResponseType(typeof(Result<Responses.VaccineResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateVaccine([FromRoute] int VaccineId, [FromBody] Command.UpdateVaccineCommand request)
    {
        var updateReport = new Command.UpdateVaccineCommand
        {
            Id = VaccineId,
            Name = request.Name,
            Description = request.Description,
            MinAge = request.MinAge
        };
        var result = await sender.Send(updateReport);
        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpDelete("{VaccineId:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVaccine([FromRoute] int VaccineId)
    {
        var result = await sender.Send(new Command.DeleteVaccineCommand
        {
            Id = VaccineId
        });
        return Ok(result);
    }
}
