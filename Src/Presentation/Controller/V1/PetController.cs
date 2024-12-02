using Contract.Abstractions.Shared;
using Contract.Services.V1.Pet;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

public class PetController : ApiController
{
    public PetController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.PetResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Pets([FromQuery] Query.GetPetQuery request)
    {
        var result = await sender.Send(
            new Query.GetPetQuery
            {
                IncludeDeleted = request.IncludeDeleted,
                SearchTerm = request.SearchTerm,
                SortColumn = request.SortColumn,
                SortOrder = request.SortOrder,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            });

        return Ok(result);
    }

    [HttpGet("{PetId:int}")]
    [ProducesResponseType(typeof(Result<Responses.PetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPetById([FromRoute] int PetId)
    {
        var result = await sender.Send(new Query.GetPetByIdQuery
        {
            Id = PetId,
        });

        return Ok(result);
    }

    [HttpGet("{UserId:Guid}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.PetResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPetByUserId([FromRoute] Guid UserId)
    {
        var result = await sender.Send(new Query.GetPetByUserIdQuery
        {
            UserId = UserId
        });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.PetResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePet([FromForm] Command.CreatePetCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPut("{PetId:int}")]
    [ProducesResponseType(typeof(Result<Responses.PetResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePet([FromRoute] int PetId, [FromForm] Command.UpdatePetCommand request)
    {
        var updatePet = new Command.UpdatePetCommand
        {
            Id = PetId,
            Name = request.Name,
            BirthDate = request.BirthDate,
            Gender = request.Gender,
            Weight = request.Weight,
            Sterilized = request.Sterilized,
            Avatar = request.Avatar,
            Description = request.Description,
            PetPhotos = request.PetPhotos,
            PetVideos = request.PetVideos,
            PetPhotosToDelete = request.PetPhotosToDelete
        };

        var result = await sender.Send(updatePet);

        return Ok(result);
    }

    [HttpDelete("{PetId:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePet([FromRoute] int PetId)
    {
        var result = await sender.Send(new Command.DeletePetCommand
        {
            Id = PetId
        });

        return Ok(result);
    }
}
