using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.PetVaccinated;
using Domain.Abstractions.Repositories;
using System.Linq.Expressions;

namespace Application.Usecases.V1.PetVaccinated.Queries
{
    public sealed class GetPetVaccinatedQueryHandler : IQueryHandler<Query.GetPetVaccinatedQuery, PagedResult<Responses.PetVaccinatedResponse>>
    {
        private readonly IPetVaccinatedRepository petVaccinatedRepository;
        private readonly IMapper mapper;

        public GetPetVaccinatedQueryHandler(
            IPetVaccinatedRepository petVaccinatedRepository,
            IMapper mapper)
        {
            this.petVaccinatedRepository = petVaccinatedRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.PetVaccinatedResponse>>> Handle(Query.GetPetVaccinatedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entities.PetVaccinated> EventsQuery;
            EventsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                ? petVaccinatedRepository.FindAll()
                    : petVaccinatedRepository.FindAll(x => x.Name.Contains(request.SearchTerm));

            if (request.PetId.HasValue)
                EventsQuery = EventsQuery.Where(x => x.PetId == request.PetId);

            var keySelector = GetSortProperty(request);

            var sortOrder = (request.SortOrder == 2) ? SortOrder.Descending : SortOrder.Ascending;

            EventsQuery = sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.PetVaccinated>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.PetVaccinatedResponse>>(Events);

            return Result.Success(result);
        }

        public static Expression<Func<Domain.Entities.PetVaccinated, object>> GetSortProperty(Query.GetPetVaccinatedQuery request)
           => request.SortColumn?.ToLower() switch
           {
               "name" => e => e.Name,
               "datevaccinated" => e => e.DateVaccinated,
               _ => e => e.Id
           };
    }
}
