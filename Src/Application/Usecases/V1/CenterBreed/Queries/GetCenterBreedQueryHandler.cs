using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.CenterBreed;
using Domain.Abstractions.Repositories;
using System.Linq.Expressions;

namespace Application.Usecases.V1.CenterBreed.Queries
{
    public sealed class GetCenterBreedQueryHandler : IQueryHandler<Query.GetCenterBreedQuery, PagedResult<Responses.CenterBreedResponse>>
    {
        private readonly ICenterBreedRepository centerBreedRepository;
        private readonly IMapper mapper;

        public GetCenterBreedQueryHandler(
            ICenterBreedRepository centerBreedRepository,
            IMapper mapper)
        {
            this.centerBreedRepository = centerBreedRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.CenterBreedResponse>>> Handle(Query.GetCenterBreedQuery request, CancellationToken cancellationToken)
        {
            var EventsQuery = GetCenterBreedQuery(request);

            if (request.Status.HasValue)
                EventsQuery = EventsQuery.Where(x => x.Status == request.Status);

            if (request.IncludeDisabled.HasValue)
            {
                if (request.IncludeDisabled == false) 
                {
                    EventsQuery = EventsQuery.Where(x => x.IsDisabled == false);
                }
            }
            else
            {
                EventsQuery = EventsQuery.Where(x => x.IsDisabled == false);
            }


            var keySelector = GetSortProperty(request);

            var sortOrder = (request.SortOrder == 2) ? SortOrder.Descending : SortOrder.Ascending;

            EventsQuery = sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.CenterBreed>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.CenterBreedResponse>>(Events);

            return Result.Success(result);
        }

        private IQueryable<Domain.Entities.CenterBreed> GetCenterBreedQuery(Query.GetCenterBreedQuery request)
        {
            IQueryable<Domain.Entities.CenterBreed> EventsQuery = string.IsNullOrEmpty(request.SearchTerm)
                ? centerBreedRepository.FindAll()
                : centerBreedRepository.FindAll(x => x.Name.Contains(request.SearchTerm));

            return EventsQuery;
        }

        public static Expression<Func<Domain.Entities.CenterBreed, object>> GetSortProperty(Query.GetCenterBreedQuery request)
         => request.SortColumn?.ToLower() switch
         {
             "price" => e => e.Price,
             _ => e => e.Id
         };
    }
}
