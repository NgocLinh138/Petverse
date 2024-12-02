using Contract.Abstractions.Shared;
using Contract.Services.V1.Schedule;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1
{
    public class ScheduleController : ApiController
    {
        public ScheduleController(ISender sender) : base(sender)
        {
        }

        [HttpPost("{ScheduleId:int}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSchedule([FromRoute] int ScheduleId, [FromForm] Command.UpdateScheduleCommand request)
        {
            var updateSchedule = new Command.UpdateScheduleCommand
            {
                Id = ScheduleId,
                Photos = request.Photos,
                Videos = request.Videos,
            };
            var result = await sender.Send(updateSchedule);
            return Ok(result);
        }

        
    }
}
