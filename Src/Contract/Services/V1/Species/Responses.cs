namespace Contract.Services.V1.Species;

public static class Responses
{
    public record SpeciesResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
    }

    public record BreedResponse
    {
        public int Id { get; init; }
        public int SpeciesId { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
    }
}
