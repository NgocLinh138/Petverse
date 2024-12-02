using Contract.Abstractions.Shared;
using Contract.Services.V1.CenterBreed;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1
{
    public class CenterBreedController : ApiController
    {
        public CenterBreedController(ISender sender) : base(sender)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<PagedResult<Responses.CenterBreedResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCenterBreeds([FromQuery] Query.GetCenterBreedQuery request)
        {
            var result = await sender.Send(
                 new Query.GetCenterBreedQuery
                 {
                     Status = request.Status,
                     IncludeDisabled = request.IncludeDisabled,
                     SearchTerm = request.SearchTerm,
                     SortColumn = request.SortColumn,
                     SortOrder = request.SortOrder,
                     PageIndex = request.PageIndex,
                     PageSize = request.PageSize,
                 });

            return Ok(result);
        }

        [HttpGet("{CenterBreedId:int}")]
        [ProducesResponseType(typeof(Result<Responses.CenterBreedResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCenterBreedById([FromRoute] int CenterBreedId)
        {
            var result = await sender.Send(new Query.GetCenterBreedByIdQuery
            {
                Id = CenterBreedId
            });

            return Ok(result);
        }


        [HttpGet("{PetCenterId:Guid}")]
        [ProducesResponseType(typeof(Result<PagedResult<Responses.CenterBreedResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCenterBreedByUserId([FromRoute] Guid PetCenterId, [FromQuery] Query.GetCenterBreedByPetCenterIdQuery request)
        {
            var result = await sender.Send(new Query.GetCenterBreedByPetCenterIdQuery
            {
                PetCenterId = PetCenterId,
                Status = request.Status,
                SearchTerm = request.SearchTerm,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            });

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<Responses.CenterBreedResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCenterBreed(Command.CreateCenterBreedCommand request)
        {
            var result = await sender.Send(request);
            return Ok(result);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPut("{CenterBreedId:int}")]
        [ProducesResponseType(typeof(Result<Responses.CenterBreedResponse>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCenterBreed([FromRoute] int CenterBreedId, Command.UpdateCenterBreedCommand request)
        {
            var updateBreed = new Command.UpdateCenterBreedCommand
            {
                Id = CenterBreedId,
                Status = request.Status,
                CancelReason = request.CancelReason
            };

            var result = await sender.Send(updateBreed);

            return Ok(result);
        }

        [HttpPut("Availability/{CenterBreedId:int}")]
        [ProducesResponseType(typeof(Result<Responses.CenterBreedResponse>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCenterBreedAvailability([FromRoute] int CenterBreedId, Command.UpdateCenterBreedAvailabilityCommand request)
        {
            var updateBreed = request with { Id = CenterBreedId };
            var result = await sender.Send(updateBreed);

            return Ok(result);
        }

        [HttpDelete("{CenterBreedId:int}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCenterBreed([FromRoute] int CenterBreedId)
        {
            var result = await sender.Send(new Command.DeleteCenterBreedCommand
            {
                Id = CenterBreedId
            });
            return Ok(result);
        }
    }
}
