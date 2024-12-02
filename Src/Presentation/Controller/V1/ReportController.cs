using Contract.Abstractions.Shared;
using Contract.Services.V1.Report;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class ReportController : ApiController
{
    public ReportController(ISender sender) : base(sender)
    {
    }

    //[Authorize(Roles = "admin, manager")]
    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ReportResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReports([FromQuery] Query.GetReportQuery request)
    {
        var result = await sender.Send(
            new Query.GetReportQuery
            {
                AppointmentId = request.AppointmentId,
                UserId = request.UserId,
                PetCenterId = request.PetCenterId,
                SearchTerm = request.SearchTerm,
                SortColumn = request.SortColumn,
                SortOrder = request.SortOrder,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            });
        return Ok(result);
    }

    [HttpGet("{ReportId:int}")]
    [ProducesResponseType(typeof(Result<Responses.ReportResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReportById([FromRoute] int ReportId)
    {
        var result = await sender.Send(new Query.GetReportByIdQuery
        {
            Id = ReportId
        });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.ReportResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReport([FromForm] Command.CreateReportCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [Authorize(Roles = "admin, manager")]
    [HttpPut("{ReportId:int}")]
    [ProducesResponseType(typeof(Result<Responses.ReportResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateReport([FromRoute] int ReportId, [FromBody] Command.UpdateReportCommand request)
    {
        var updateReport = new Command.UpdateReportCommand
        {
            Id = ReportId,
            Status = request.Status
        };
        var result = await sender.Send(updateReport);
        return Ok(result);
    }

    [HttpDelete("{ReportId:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReport([FromRoute] int ReportId)
    {
        var result = await sender.Send(new Command.DeleteReportCommand
        {
            Id = ReportId
        });
        return Ok(result);
    }
}
