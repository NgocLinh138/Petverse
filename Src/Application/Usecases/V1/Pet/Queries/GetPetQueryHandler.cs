using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.Pet;
using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Usecases.V1.Pet.Queries
{
    public sealed class GetPetQueryHandler : IQueryHandler<Query.GetPetQuery, PagedResult<Responses.PetResponse>>
    {
        private readonly IPetRepository petRepository;
        private readonly IMapper mapper;

        public GetPetQueryHandler(
            IPetRepository petRepository,
            IMapper mapper)
        {
            this.petRepository = petRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.PetResponse>>> Handle(Query.GetPetQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entities.Pet> EventsQuery = petRepository.FindAll().Include(p => p.PetVaccinateds);


            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                EventsQuery = EventsQuery.Where(x => x.Name.Contains(request.SearchTerm));
            }

            if (request.IncludeDeleted.HasValue)
            {
                if (request.IncludeDeleted.Value == false) 
                {
                    EventsQuery = EventsQuery.Where(x => !x.DeletedDate.HasValue);
                }
            }
            else
            {
                EventsQuery = EventsQuery.Where(x => !x.DeletedDate.HasValue);
            }

            var keySelector = GetSortProperty(request);

            var sortOrder = (request.SortOrder == 2) ? SortOrder.Descending : SortOrder.Ascending;

            EventsQuery = sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.Pet>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.PetResponse>>(Events);

            return Result.Success(result);
        }

        public static Expression<Func<Domain.Entities.Pet, object>> GetSortProperty(Query.GetPetQuery request)
            => request.SortColumn?.ToLower() switch
            {
                "weight" => e => e.Weight,
                _ => e => e.Id
            };
    }
}
