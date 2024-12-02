using Contract.Abstractions.Shared;
using Contract.Services.V1.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class DashboardController : ApiController
{
    public DashboardController(ISender sender) : base(sender)
    {
    }

    [Authorize(Roles = "admin")]
    [HttpGet("Admin/OverView")]
    [ProducesResponseType(typeof(Result<Responses.OverviewAdminResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Overview()
    {
        var result = await sender.Send(new Query.GetOverViewAdminQuery());

        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("Admin/LineChart")]
    [ProducesResponseType(typeof(Result<Responses.LineChartAdminResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LineChart()
    {
        var result = await sender.Send(new Query.GetLineChartAdminQuery());

        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("Admin/PipeChart")]
    [ProducesResponseType(typeof(Result<Responses.PipeChartAdminResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PipeChart()
    {
        var result = await sender.Send(new Query.GetPipeChartAdminQuery());

        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("Admin/BarChart")]
    [ProducesResponseType(typeof(Result<Responses.BarChartAdminResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BarChart()
    {
        var result = await sender.Send(new Query.GetBarChartAdminQuery());

        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("Admin/SummeryTable")]
    [ProducesResponseType(typeof(Result<Responses.OverviewAdminResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SummeryTable()
    {
        var result = await sender.Send(new Query.GetSummeryTableAdminQuery());

        return Ok(result);
    }

    // Manager
    [Authorize(Roles = "manager")]
    [HttpGet("Manager/OverView")]
    [ProducesResponseType(typeof(Result<Responses.OverviewManagerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> OverviewManager()
    {
        var result = await sender.Send(new Query.GetOverViewManagerQuery());

        return Ok(result);
    }

    [Authorize(Roles = "manager")]
    [HttpGet("Manager/LineChart")]
    [ProducesResponseType(typeof(Result<Responses.LineChartManagerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LineChartManager()
    {
        var result = await sender.Send(new Query.GetLineChartManagerQuery());

        return Ok(result);
    }

    [Authorize(Roles = "manager")]
    [HttpGet("Manager/BarChart")]
    [ProducesResponseType(typeof(Result<Responses.BarChartManagerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BarChartManager()
    {
        var result = await sender.Send(new Query.GetBarChartManagerQuery());

        return Ok(result);
    }
}
