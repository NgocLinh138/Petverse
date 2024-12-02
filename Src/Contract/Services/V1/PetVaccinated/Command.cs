using Contract.Abstractions.Message;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.PetVaccinated
{
    public class Command
    {
        public record CreatePetVaccinatedCommand : ICommand<Responses.PetVaccinatedResponse>
        {
            public int PetId { get; init; }
            public string Name { get; init; } = null!;
            public IFormFile Image { get; init; } = null!;
            public string DateVaccinated { get; init; }
        }

        public record UpdatePetVaccinatedCommand : ICommand<Responses.PetVaccinatedResponse>
        {
            public int Id { get; init; }
            public string Name { get; init; } = null!;
            public IFormFile? Image { get; init; } = null!;
            public string DateVaccinated { get; init; }
        }

        public record DeletePetVaccinatedCommand : ICommand
        {
            public int Id { get; init; }
        }
    }
}
