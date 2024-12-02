using Contract.Enumerations;
using static Contract.Services.V1.Species.Responses;

namespace Contract.Services.V1.Place
{
    public static class Responses
    {
        public record PlaceResponse
        {
            public int Id { get; set; }
            public PlaceType Type { get; set; }
            public string Name { get; set; }
            public string Lat { get; set; }
            public string Lng { get; set; }
            public string Address { get; set; }
            public string Image { get; set; }
            public string? Description { get; set; }
            public List<SpeciesResponse> Species { get; set; } = new();
        }
    }
}
