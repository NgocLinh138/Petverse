using Contract.Abstractions.Message;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.Certification
{
    public class Command
    {
        public record CreateCertificationCommand : ICommand<Responses.CertificationResponse>
        {
            public int ApplicationId { get; init; }
            public IFormFile Image { get; init; } = null!;
        }

        public record UpdateCertificationCommand : ICommand<Responses.CertificationResponse>
        {
            public int Id { get; init; }
            public int ApplicationId { get; init; }
            public IFormFile Image { get; init; } = null!;
        }

        public record DeleteCertificationCommand : ICommand<Responses.CertificationResponse>
        {
            public int Id { get; init; }
        }
    }
}
