using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetCenterService;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.PetCenterService.Queries
{
    public sealed class GetPetCenterServiceQueryHandler : IQueryHandler<Query.GetPetCenterServiceQuery, PagedResult<Responses.PetCenterServiceResponse>>
    {
        private readonly IPetCenterServiceRepository petCenterServiceRepository;
        private readonly IMapper mapper;

        public GetPetCenterServiceQueryHandler(
            IPetCenterServiceRepository petCenterServiceRepository,
            IMapper mapper)
        {
            this.petCenterServiceRepository = petCenterServiceRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.PetCenterServiceResponse>>> Handle(Query.GetPetCenterServiceQuery request, CancellationToken cancellationToken)
        {
            var EventsQuery = petCenterServiceRepository.FindAll();

            if (request.Type.HasValue)
                EventsQuery = EventsQuery.Where(x => x.Type == request.Type);

            if (request.PetCenterId.HasValue)
                EventsQuery = EventsQuery.Where(x => x.PetCenterId == request.PetCenterId.Value);

            //var keySelector = GetSortProperty(request);

            //var sortOrder = (request.SortOrder == 2) ? SortOrder.Descending : SortOrder.Ascending;

            //EventsQuery = sortOrder == SortOrder.Descending
            //    ? EventsQuery.OrderByDescending(keySelector)
            //    : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.PetCenterService>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.PetCenterServiceResponse>>(Events);

            return Result.Success(result);
        }

        //public static Expression<Func<Domain.Entities.PetCenterService, object>> GetSortProperty(Query.GetPetCenterServiceQuery request)
        //  => request.SortColumn?.ToLower() switch
        //  {
        //      "rate" => e => e.Rate,
        //      "price" => e => e.Price,
        //      _ => e => e.Id
        //  };
    }
}
