using Contract.Abstractions.Message;
using Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.CenterBreed
{
    public class Command
    {
        public record CreateCenterBreedCommand : ICommand<Responses.CenterBreedResponse>
        {
            public Guid PetCenterId { get; init; }
            public int SpeciesId { get; init; }
            public string Name { get; init; }
            public string Description { get; init; }
            public decimal Price { get; init; }
            public List<IFormFile>? Images { get; init; }
        }

        public record UpdateCenterBreedCommand : ICommand<Responses.CenterBreedResponse>
        {
            public int Id { get; init; }
            public CenterBreedStatus Status { get; init; }
            public string? CancelReason { get; init; }
        }

        public record UpdateCenterBreedAvailabilityCommand : ICommand<Responses.CenterBreedResponse>
        {
            public int Id { get; init; }
            public bool IsDisabled { get; init; }
        }

        public record DeleteCenterBreedCommand : ICommand
        {
            public int Id { get; init; }
        }
    }
}
