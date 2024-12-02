using Contract.Abstractions.Message;

namespace Contract.Services.V1.PetService
{
    public static class Command
    {
        public record CreatePetServiceCommand : ICommand<Responses.PetServiceResponse>
        {
            public string Name { get; init; } = null!;
            public string? Description { get; init; }
        }

        public record UpdatePetServiceCommand : ICommand<Responses.PetServiceResponse>
        {
            public int Id { get; init; }
            public string Name { get; init; } = null!;
            public string? Description { get; init; }
        }

        public record DeletePetServiceCommand : ICommand
        {
            public int Id { get; init; }
        }
    }
}
