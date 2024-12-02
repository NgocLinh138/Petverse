using Contract.Enumerations;
using static Contract.Services.V1.Job.Responses;

namespace Contract.Services.V1.PetCenter;

public static class Responses
{
    public record PetCenterResponse
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public string Name { get; init; }
        public string Location { get; init; }
        public bool HaveTransport { get; init; }
        public int NumPet { get; init; }
    }

    public record PetCenterGetAllResponse
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string PhoneNumber { get; init; }
        public string Avatar { get; init; }
        public float Rate { get; init; }
        public IEnumerable<string> Pets { get; init; }
        public IEnumerable<string> PetCenterServices { get; init; }
        public bool IsVerified { get; init; }
        public int Yoe { get; init; }
    };

    public record PetCenterGetByIdResponse
    {
        public Guid Id { get; init; }
        public int ApplicationId { get; init; }
        public string Name { get; init; }
        public int NumPet { get; init; }
        public bool IsVerified { get; init; }
        public string PhoneNumber { get; init; }
        public string Address { get; init; }
        public string Avatar { get; init; }
        public string Description { get; init; }
        public int Yoe { get; init; }
        public float Rate { get; init; }
        public JobResponse Job { get; init; }
        public IEnumerable<string> Species { get; init; }
        public IEnumerable<PetCenterServiceResponse> PetCenterServices { get; init; }
        public ICollection<CertificationResponse> Certifications { get; init; }
    }

    public record PetCenterServiceResponse(
        int PetCenterServiceId,
        string Name,
        decimal Price,
        string Description,
        ServiceType Type,
        float Rate,
        int NumRate);
    public record CertificationResponse(
        int Id,
        string Image
        );

    public record TopPetCenterResponse(ICollection<TopPetCenterData> TopPetCenterDatas);
    public record TopPetCenterData(
        Guid PetCenterId,
        string Avatar,
        string Name,
        float AverageRate);
}

