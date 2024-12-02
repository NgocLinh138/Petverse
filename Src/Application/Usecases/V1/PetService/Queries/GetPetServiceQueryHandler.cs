using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.PetService;
using Domain.Abstractions.Repositories;
using System.Linq.Expressions;

namespace Application.Usecases.V1.PetService.Queries
{
    public sealed class GetPetServiceQueryHandler : IQueryHandler<Query.GetPetServiceQuery, PagedResult<Responses.PetServiceResponse>>
    {
        private readonly IPetServiceRepository petServiceRepository;
        private readonly IMapper mapper;

        public GetPetServiceQueryHandler(
            IPetServiceRepository petServiceRepository,
            IMapper mapper)
        {
            this.petServiceRepository = petServiceRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.PetServiceResponse>>> Handle(Query.GetPetServiceQuery request, CancellationToken cancellationToken)
        {
            var EventsQuery = petServiceRepository.FindAll();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                EventsQuery = EventsQuery.Where(x => x.Name.ToLower().Contains(request.SearchTerm.ToLower()));
            }

            var keySelector = GetSortProperty(request);

            var Events = await PagedResult<Domain.Entities.PetService>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.PetServiceResponse>>(Events);

            return Result.Success(result);
        }

        public static Expression<Func<Domain.Entities.PetService, object>> GetSortProperty(Query.GetPetServiceQuery request)
            => request.SortColumn?.ToLower() switch
            {
                "name" => e => e.Name,
                _ => e => e.Name
            };
    }
}
