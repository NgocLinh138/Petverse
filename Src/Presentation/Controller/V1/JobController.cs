using Contract.Abstractions.Shared;
using Contract.Services.V1.Job;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class JobController : ApiController
{
    public JobController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.JobResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Jobs([FromQuery] Query.GetJobQuery request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpGet("{PetCenterId:Guid}")]
    [ProducesResponseType(typeof(Result<Responses.JobResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Jobs([FromRoute] Guid PetCenterId)
    {
        var result = await sender.Send(new Query.GetJobByPetCenterIdQuery { PetCenterId = PetCenterId });
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.JobResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Jobs(Command.CreateJobCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPut("{JobId:Guid}")]
    [ProducesResponseType(typeof(Result<Responses.JobResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Jobs([FromRoute] Guid JobId, Command.UpdateJobCommand request)
    {
        var updateJob = request with { Id = JobId };
        var result = await sender.Send(updateJob);
        return Ok(result);
    }

    [HttpDelete("{JobId:Guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteJob([FromRoute] Guid JobId)
    {
        var result = await sender.Send(new Command.DeleteJobCommand(JobId));
        return Ok(result);
    }
}
