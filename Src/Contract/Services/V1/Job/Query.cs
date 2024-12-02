using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;

namespace Contract.Services.V1.Job;
public static class Query
{
    public record GetJobQuery : IQuery<PagedResult<Responses.JobResponse>>
    {
        public int? PetServiceId { get; set; }
        public string? SearchTerm { get; set; }
        public int PageIndex { get; init; } = 1;
        public int PageSize { get; init; } = 10;

    }

    public record GetJobByPetCenterIdQuery : IQuery<Responses.JobResponse>
    {
        public Guid PetCenterId { get; init; }
    }

}
