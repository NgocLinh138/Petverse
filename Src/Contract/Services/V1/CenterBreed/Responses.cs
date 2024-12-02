using Contract.Enumerations;

namespace Contract.Services.V1.CenterBreed
{
    public static class Responses
    {

        public record CenterBreedResponse
        {
            public int Id { get; set; }
            public Guid PetCenterId { get; set; }
            public string PetCenterName { get; set; } = null!;
            public int SpeciesId { get; set; }
            public string SpeciesName { get; set; } = null!;
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public CenterBreedStatus Status { get; set; }
            public string? CancelReason { get; set; }
            public bool IsDisable { get; init; }
            public List<CenterBreedImageResponse>? Images { get; set; }
        }

        public record CenterBreedImageResponse
        {
            public int CenterBreedImageId { get; set; }
            public string Image { get; set; } = null!;
        }
    }
}
