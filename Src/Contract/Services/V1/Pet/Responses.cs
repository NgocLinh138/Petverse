using Contract.Enumerations;
using static Contract.Services.V1.PetVaccinated.Responses;

namespace Contract.Services.V1.Pet
{
    public static class Responses
    {
        public record PetResponse
        {
            public int Id { get; set; }
            public Guid UserId { get; set; }
            public int SpeciesId { get; set; }
            public int BreedId { get; set; }
            public string Name { get; set; } = null!;
            public string BirthDate { get; set; } 
            public Gender Gender { get; set; }
            public float Weight { get; set; }
            public bool Sterilized { get; set; }
            public string Avatar { get; set; } = null!;
            public string? Description { get; set; }
            public List<PetPhotoResponse> PetPhotos { get; set; } = new();
            public List<PetVaccinatedByPetResponse> PetVaccinateds { get; set; } = new(); 
        }

        public record PetPhotoResponse
        {
            public int PetPhotoId { get; set; }
            public string Url { get; set; } = null!;
            public MediaType Type { get; set; }
        }
    }
}
