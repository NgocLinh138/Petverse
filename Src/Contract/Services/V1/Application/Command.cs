using Contract.Abstractions.Message;
using Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.Application
{
    public class Command
    {
        public record CreateApplicationCommand : ICommand<Responses.ApplicationResponse>
        {
            public Guid UserId { get; init; }
            public string Name { get; init; } = null!;
            public string PhoneNumber { get; init; } = null!;
            public string Address { get; init; } = null!;
            public IFormFile Image { get; init; } = null!;
            public string Description { get; init; } = null!;
            public ICollection<IFormFile>? Certifications { get; set; }
            public List<int>? PetServiceIds { get; set; } = new();
        }

        public record UpdateApplicationStatusCommand : ICommand<Responses.ApplicationResponse>
        {
            public int Id { get; init; }
            public Guid RoleId { get; init; }
            public JobApplicationStatus Status { get; init; }
            public string? CancelReason { get; init; }
            public bool IsVerified { get; set; }
        }

        public record DeleteApplicationCommand : ICommand
        {
            public int Id { get; init; }
        }
    }
}
