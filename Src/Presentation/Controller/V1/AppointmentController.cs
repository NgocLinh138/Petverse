using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.Appointment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class AppointmentController : ApiController
{
    public AppointmentController(ISender sender) : base(sender)
    {
    }

    [HttpGet()]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.AppointmentResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ServiceAppointmentsByUserId([FromQuery] Query.GetAppointmentQuery request)
    {
        var result = await sender.Send(request);
        if (!result.IsSuccess)
            return HandlerFailure(result);
        return Ok(result);
    }

    [HttpGet("{AppointmentId:Guid}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.AppointmentByIdResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AppointmentsByUserId([FromRoute] Guid AppointmentId, AppointmentType Type)
    {
        var result = await sender.Send(new Query.GetAppointmentByIdQuery(AppointmentId, Type));
        if (!result.IsSuccess)
            return HandlerFailure(result);
        return Ok(result);
    }

    [HttpGet("BreedAppointment/{UserId:Guid}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.BreedAppointmetByUserResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BreedAppointemntsByUserId([FromRoute] Guid UserId)
    {
        var result = await sender.Send(new Query.GetBreedAppointmentByUserIdQuery(UserId));
        if (!result.IsSuccess)
            return HandlerFailure(result);
        return Ok(result);
    }

    [HttpPost("BreedAppointment")]
    [ProducesResponseType(typeof(Result<Responses.BreedAppointmentResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BreedAppointments(Command.CreateBreedAppointmentCommand request)
    {
        var result = await sender.Send(request);
        if (!result.IsSuccess)
            return HandlerFailure(result);
        return Ok(result);
    }

    [HttpPost("ServiceAppointment")]
    [ProducesResponseType(typeof(Result<Responses.ServiceAppointmentResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ServiceAppointments(Command.CreateServiceAppointmentCommand request)
    {
        var result = await sender.Send(request);
        if (!result.IsSuccess)
            return HandlerFailure(result);
        return Ok(result);
    }

    [HttpPut("{AppointmentId:Guid}/Status")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Appointments([FromRoute] Guid AppointmentId, Command.UpdateStatusCommand request)
    {
        var updateAppointment = request with { Id = AppointmentId };
        var result = await sender.Send(updateAppointment);

        if (!result.IsSuccess)
            return HandlerFailure(result);
        return Ok(result);
    }

    [HttpDelete("{AppointmentId:Guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Appointments([FromRoute] Guid BreedAppointmentId)
    {
        var result = await sender.Send(new Command.DeleteAppointmentCommand(BreedAppointmentId));
        if (!result.IsSuccess)
            return HandlerFailure(result);
        return Ok(result);
    }
}
