using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.Vaccine;
using Domain.Abstractions.Repositories;
using System.Linq.Expressions;

namespace Application.Usecases.V1.Vaccine.Queries
{
    public sealed class GetVaccineQueryHandler : IQueryHandler<Query.GetVaccineQuery, PagedResult<Responses.VaccineResponse>>
    {
        private readonly IVaccineRepository vaccineRepository;
        private readonly IMapper mapper;

        public GetVaccineQueryHandler(
            IVaccineRepository vaccineRepository,
            IMapper mapper)
        {
            this.vaccineRepository = vaccineRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.VaccineResponse>>> Handle(Query.GetVaccineQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entities.Vaccine> EventsQuery;
            EventsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                ? vaccineRepository.FindAll()
                : vaccineRepository.FindAll(x => x.Name.Contains(request.SearchTerm));

            if (request.SpeciesId.HasValue)
            {
                EventsQuery = EventsQuery.Where(x => x.SpeciesId == request.SpeciesId);
            }

            var keySelector = GetSortProperty(request);

            var sortOrder = (request.SortOrder == 2) ? SortOrder.Descending : SortOrder.Ascending;

            EventsQuery = sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.Vaccine>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.VaccineResponse>>(Events);

            return Result.Success(result);
        }

        public static Expression<Func<Domain.Entities.Vaccine, object>> GetSortProperty(Query.GetVaccineQuery request)
           => request.SortColumn?.ToLower() switch
           {
               "name" => e => e.Name,
               "monthage" => e => e.MinAge,
               _ => e => e.Id
           };
    }
}
