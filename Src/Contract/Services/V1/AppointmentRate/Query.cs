using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;

namespace Contract.Services.V1.AppointmentRate
{
    public static class Query
    {
        public record GetAppointmentRateByPetCenterIdQuery : IQuery<PagedResult<Responses.AppointmentRateResponse>>
        {
            public Guid PetCenterId { get; init; }
            public string? SortColumn { get; init; }
            public int? SortOrder { get; set; }
            public int PageIndex { get; init; }
            public int PageSize { get; init; }
        }

        public record GetAppointmentRateByCenterBreedIdQuery : IQuery<PagedResult<Responses.AppointmentRateResponse>>
        {
            public int CenterBreedId { get; init; }
            public string? SortColumn { get; init; }
            public int? SortOrder { get; set; }
            public int PageIndex { get; init; }
            public int PageSize { get; init; }
        }

        public record GetAppointmentRateByIdQuery : IQuery<Responses.AppointmentRateResponse>
        {
            public int Id { get; init; }
        }
    }
}
