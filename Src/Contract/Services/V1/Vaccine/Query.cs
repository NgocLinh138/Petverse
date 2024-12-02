using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;

namespace Contract.Services.V1.Vaccine
{
    public static class Query
    {
        public record GetVaccineQuery : IQuery<PagedResult<Responses.VaccineResponse>>
        {
            public int? SpeciesId { get; init; }
            public string? SearchTerm { get; init; }
            public string? SortColumn { get; init; }
            public int? SortOrder { get; init; }
            public int PageIndex { get; init; }
            public int PageSize { get; init; }
        }

        public record GetVaccineByIdQuery : IQuery<Responses.VaccineResponse>
        {
            public int Id { get; init; }
        }
    }
}
