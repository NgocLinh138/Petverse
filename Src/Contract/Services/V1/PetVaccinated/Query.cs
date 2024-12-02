using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;

namespace Contract.Services.V1.PetVaccinated
{
    public static class Query
    {
        public record GetPetVaccinatedQuery : IQuery<PagedResult<Responses.PetVaccinatedResponse>>
        {
            public int? PetId { get; init; }
            public string? SearchTerm { get; init; }
            public string? SortColumn { get; init; }
            public int? SortOrder { get; init; }
            public int PageIndex { get; init; }
            public int PageSize { get; init; }
        }

        public record GetPetVaccinatedByIdQuery : IQuery<Responses.PetVaccinatedResponse>
        {
            public int Id { get; init; }
        }
    }
}
