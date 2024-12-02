using Contract.Enumerations;

namespace Contract.Services.V1.Application
{
    public static class Responses
    {
        public record ApplicationResponse
        {
            public int Id { get; set; }
            public Guid UserId { get; set; }
            public string Name { get; set; } = null!;
            public string PhoneNumber { get; set; } = null!;
            public string Address { get; set; } = null!;
            public string Avatar { get; set; } = null!;
            public string Description { get; set; } = null!;
            public List<string> Certifications { get; set; }
            public string? CancelReason { get; set; }
            public JobApplicationStatus Status { get; set; }
            public List<ApplicationPetServiceResponse> ApplicationPetServices { get; set; } = new List<ApplicationPetServiceResponse>();
        }

        public record ApplicationPetServiceResponse
        {
            public int PetServiceId { get; set; }
        }
    }
}
