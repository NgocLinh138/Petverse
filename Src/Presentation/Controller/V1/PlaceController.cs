using Contract.Abstractions.Shared;
using Contract.Services.V1.Place;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1
{
    public class PlaceController : ApiController
    {
        public PlaceController(ISender sender) : base(sender)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<PagedResult<Responses.PlaceResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPlaces([FromQuery] Query.GetPlaceQuery request)
        {
            var result = await sender.Send(
                 new Query.GetPlaceQuery
                 {
                     SpeciesId = request.SpeciesId,
                     Type = request.Type,
                     SearchTerm = request.SearchTerm,
                     SortColumn = request.SortColumn,
                     SortOrder = request.SortOrder,
                     PageIndex = request.PageIndex,
                     PageSize = request.PageSize,
                 });

            return Ok(result);
        }

        [HttpGet("{PlaceId:int}")]
        [ProducesResponseType(typeof(Result<Responses.PlaceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlaceById([FromRoute] int PlaceId)
        {
            var result = await sender.Send(new Query.GetPlaceByIdQuery
            {
                Id = PlaceId
            });

            return Ok(result);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        [ProducesResponseType(typeof(Result<Responses.PlaceResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePlace([FromForm] Command.CreatePlaceCommand request)
        {
            var result = await sender.Send(request);
            return Ok(result);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPut("{PlaceId:int}")]
        [ProducesResponseType(typeof(Result<Responses.PlaceResponse>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateApplication([FromRoute] int PlaceId, [FromForm] Command.UpdatePlaceCommand request)
        {
            var updatePlace = request with { Id = PlaceId };
            var result = await sender.Send(updatePlace);
            return Ok(result);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpDelete("{PlaceId:int}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePlace([FromRoute] int PlaceId)
        {
            var result = await sender.Send(new Command.DeletePlaceCommand
            {
                Id = PlaceId
            });
            return Ok(result);
        }
    }
}
