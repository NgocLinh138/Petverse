using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.Place;
using Domain.Abstractions.Repositories;
using System.Linq.Expressions;

namespace Application.Usecases.V1.Place.Queries
{
    public sealed class GetPlaceQueryHandler : IQueryHandler<Query.GetPlaceQuery, PagedResult<Responses.PlaceResponse>>
    {
        private readonly IPlaceRepository placeRepository;
        private readonly IMapper mapper;

        public GetPlaceQueryHandler(
            IPlaceRepository placeRepository,
            IMapper mapper)
        {
            this.placeRepository = placeRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.PlaceResponse>>> Handle(Query.GetPlaceQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entities.Place> EventsQuery = string.IsNullOrEmpty(request.SearchTerm)
              ? placeRepository.FindAll()
              : placeRepository.FindAll(x => x.Name.Contains(request.SearchTerm));

            if (request.SpeciesId.HasValue)
                EventsQuery = EventsQuery.Where(p => p.SpeciesPlaces.Any(sp => sp.SpeciesId == request.SpeciesId));

            if (request.Type.HasValue)
                EventsQuery = EventsQuery.Where(x => x.Type == request.Type);

            var keySelector = GetSortProperty(request);

            var sortOrder = (request.SortOrder == 2) ? SortOrder.Descending : SortOrder.Ascending;

            EventsQuery = sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.Place>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.PlaceResponse>>(Events);

            return Result.Success(result);
        }

        public static Expression<Func<Domain.Entities.Place, object>> GetSortProperty(Query.GetPlaceQuery request)
            => request.SortColumn?.ToLower() switch
            {
                "name" => e => e.Name,
                _ => e => e.Id
            };
    }
}
