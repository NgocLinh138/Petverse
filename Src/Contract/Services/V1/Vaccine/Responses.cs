namespace Contract.Services.V1.Vaccine
{
    public static class Responses
    {
        public record VaccineResponse
        {
            public int Id { get; set; }
            public int SpeciesId { get; set; }
            public string Name { get; set; }
            public string? Description { get; set; }
            public int MinAge { get; set; }
        }
    }
}
