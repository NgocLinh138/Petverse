namespace Contract.Services.V1.PetVaccinated
{
    public static class Responses
    {
        public record PetVaccinatedResponse
        {
            public int Id { get; set; } 
            public int PetId { get; set; }
            public string Name { get; set; } = null!;
            public string Image { get; set; } = null!;
            public string DateVaccinated { get; set; }
        }

        public record PetVaccinatedByPetResponse
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Image { get; set; } = null!;
            public string DateVaccinated { get; set; }
        }
    }
}
