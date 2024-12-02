using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.Application;
using Domain.Abstractions.Repositories;
using System.Linq.Expressions;

namespace Application.Usecases.V1.Application.Queries
{
    public sealed class GetApplicationQueryHandler : IQueryHandler<Query.GetApplicationQuery, PagedResult<Responses.ApplicationResponse>>
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly IMapper mapper;

        public GetApplicationQueryHandler(
            IApplicationRepository applicationRepository,
            IMapper mapper)
        {
            this.applicationRepository = applicationRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.ApplicationResponse>>> Handle(Query.GetApplicationQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entities.Application> EventsQuery = string.IsNullOrEmpty(request.SearchTerm)
                ? applicationRepository.FindAll()
                : applicationRepository.FindAll(x => x.Name.Contains(request.SearchTerm));

            if (request.UserId.HasValue)
                EventsQuery = EventsQuery.Where(x => x.UserId == request.UserId);

            if (request.Status.HasValue)
                EventsQuery = EventsQuery.Where(x => x.Status == request.Status);

            var keySelector = GetSortProperty(request);

            var sortOrder = (request.SortOrder == 2) ? SortOrder.Descending : SortOrder.Ascending;

            EventsQuery = sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.Application>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.ApplicationResponse>>(Events);

            return Result.Success(result);
        }

        public static Expression<Func<Domain.Entities.Application, object>> GetSortProperty(Query.GetApplicationQuery request)
         => request.SortColumn?.ToLower() switch
         {
             "yoe" => e => e.Yoe,
             "name" => e => e.Name,
             _ => e => e.Id
         };
    }
}
