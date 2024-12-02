using Contract.Abstractions.Message;

namespace Contract.Services.V1.Vaccine
{
    public class Command
    {
        public record CreateVaccineCommand : ICommand<Responses.VaccineResponse>
        {
            public int SpeciesId { get; set; }
            public string Name { get; set; }
            public string? Description { get; set; }
            public int MinAge { get; set; }
        }

        public record UpdateVaccineCommand : ICommand<Responses.VaccineResponse>
        {
            public int Id { get; init; }
            public string Name { get; set; }
            public string? Description { get; set; }
            public int MinAge { get; set; }
        }

        public record DeleteVaccineCommand : ICommand
        {
            public int Id { get; init; }
        }
    }
}
