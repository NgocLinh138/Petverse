using Contract.Abstractions.Message;
using Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.Pet
{
    public class Command
    {
        public record CreatePetCommand : ICommand<Responses.PetResponse>
        {
            public Guid UserId { get; init; }
            public int SpeciesId { get; init; }
            public int BreedId { get; init; }
            public string Name { get; init; } = null!;
            public string BirthDate { get; init; }
            public Gender Gender { get; init; }
            public float Weight { get; init; }
            public bool Sterilized { get; init; }
            public IFormFile? Avatar { get; init; }
            public string? Description { get; init; } = null;
            public ICollection<IFormFile>? PetPhotos { get; init; }
            public ICollection<IFormFile>? PetVideos { get; init; }
        }
   
        public record UpdatePetCommand : ICommand<Responses.PetResponse>
        {
            public int Id { get; init; }
            public string? Name { get; init; } = null!;
            public string? BirthDate { get; init; }
            public Gender? Gender { get; init; }
            public float? Weight { get; init; }
            public bool? Sterilized { get; init; }
            public IFormFile? Avatar { get; init; } = null!;
            public string? Description { get; init; } = null;
            public ICollection<IFormFile>? PetPhotos { get; init; }
            public ICollection<IFormFile>? PetVideos { get; init; }
            public List<int>? PetPhotosToDelete { get; init; }
        }

        public record DeletePetCommand : ICommand
        {
            public int Id { get; init; }
        }
    }
}
