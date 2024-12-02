using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;

namespace Contract.Services.V1.PetCenterService
{
    public static class Query
    {
        public record GetPetCenterServiceQuery : IQuery<PagedResult<Responses.PetCenterServiceResponse>>
        {
            public ServiceType? Type { get; init; }
            public Guid? PetCenterId { get; init; }
            public string? SortColumn { get; init; }
            public int? SortOrder { get; set; }
            public int PageIndex { get; init; }
            public int PageSize { get; init; }
        }

        public record GetPetCenterServiceByIdQuery : IQuery<Responses.PetCenterServiceResponse>
        {
            public int Id { get; init; }
        }
    }
}
