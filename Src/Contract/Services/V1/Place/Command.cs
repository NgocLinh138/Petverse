using Contract.Abstractions.Message;
using Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.Place
{
    public class Command
    {
        public record CreatePlaceCommand : ICommand<Responses.PlaceResponse>
        {
            public PlaceType Type { get; init; }
            public string Name { get; init; }
            public string Lat { get; init; }
            public string Lng { get; init; }
            public string Address { get; init; }
            public IFormFile Image { get; init; } = null!;
            public string? Description { get; init; }
            public bool IsFree { get; init; }
            public List<int> SpeciesIds { get; init; } = new();
        }

        public record UpdatePlaceCommand : ICommand<Responses.PlaceResponse>
        {
            public int Id { get; init; }
            public PlaceType? Type { get; init; }
            public string? Name { get; init; }
            public string? Lat { get; init; }
            public string? Lng { get; init; }
            public string? Address { get; init; }
            public IFormFile? Image { get; init; } = null!;
            public string? Description { get; init; }
            public bool? IsFree { get; init; }
            public List<int>? SpeciesIds { get; init; } = new();
        }

        public record DeletePlaceCommand : ICommand
        {
            public int Id { get; init; }
        }
    }
}
