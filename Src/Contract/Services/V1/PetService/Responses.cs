namespace Contract.Services.V1.PetService
{
    public static class Responses
    {
        public record PetServiceResponse
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string? Description { get; set; }
        }
    }
}
