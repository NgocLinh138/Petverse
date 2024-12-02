using Contract.Abstractions.Shared;
using Contract.Services.V1.AppointmentRate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class AppointmentRateController : ApiController
{
    public AppointmentRateController(ISender sender) : base(sender)
    {
    }

    [HttpGet("PetCenter/{PetCenterId:Guid}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.AppointmentRateResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAppointmentRatesByPetCenter([FromRoute] Guid PetCenterId, [FromQuery] Query.GetAppointmentRateByPetCenterIdQuery request)
    {
        var result = await sender.Send(new Query.GetAppointmentRateByPetCenterIdQuery
        {
            PetCenterId = PetCenterId,
            SortColumn = request.SortColumn,
            SortOrder = request.SortOrder,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
        return Ok(result);
    }

    [HttpGet("CenterBreed/{CenterBreedId:int}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.AppointmentRateResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAppointmentRatesByCenterBreed([FromRoute] int CenterBreedId, [FromQuery] Query.GetAppointmentRateByCenterBreedIdQuery request)
    {
        var result = await sender.Send(new Query.GetAppointmentRateByCenterBreedIdQuery
        {
            CenterBreedId = CenterBreedId,
            SortColumn = request.SortColumn,
            SortOrder = request.SortOrder,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
        return Ok(result);
    }

    [HttpGet("{AppointmentRateId:int}")]
    [ProducesResponseType(typeof(Result<Responses.AppointmentRateResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAppointmentRateById([FromRoute] int AppointmentRateId)
    {
        var result = await sender.Send(new Query.GetAppointmentRateByIdQuery
        {
            Id = AppointmentRateId
        });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.AppointmentRateResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAppointmentRate(Command.CreateAppointmentRateCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPut("{AppointmentRateId:int}")]
    [ProducesResponseType(typeof(Result<Responses.AppointmentRateResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAppointmentRate([FromRoute] int AppointmentRateId, [FromForm] Command.UpdateAppointmentRateCommand request)
    {
        var updateAppointmentRate = new Command.UpdateAppointmentRateCommand
        {
            Id = AppointmentRateId,
            Rate = request.Rate,
            Description = request.Description
        };
        var result = await sender.Send(updateAppointmentRate);
        return Ok(result);
    }

    [HttpDelete("{AppointmentRateId:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAppointmentRate([FromRoute] int AppointmentRateId)
    {
        var result = await sender.Send(new Command.DeleteAppointmentRateCommand
        {
            Id = AppointmentRateId
        });
        return Ok(result);
    }
}
