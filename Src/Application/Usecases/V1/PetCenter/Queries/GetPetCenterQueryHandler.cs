using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetCenter;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.PetCenter.Queries;
public sealed class GetPetCenterQueryHandler : IQueryHandler<Query.GetPetCenterQuery, PagedResult<Responses.PetCenterGetAllResponse>>
{
    private readonly IPetCenterRepository petCenterRepository;
    private readonly IMapper mapper;

    public GetPetCenterQueryHandler(
        IPetCenterRepository petCenterRepository,
        IMapper mapper
        )
    {
        this.petCenterRepository = petCenterRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.PetCenterGetAllResponse>>> Handle(Query.GetPetCenterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var EventsQuery = GetPetCentersQuery(request);

            //var keySelector = GetSortProperty(request);

            var Events = await PagedResult<Domain.Entities.PetCenter>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.PetCenterGetAllResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private IQueryable<Domain.Entities.PetCenter> GetPetCentersQuery(Query.GetPetCenterQuery request)
    {
        IQueryable<Domain.Entities.PetCenter> EventsQuery = string.IsNullOrEmpty(request.SearchTerm)
            ? petCenterRepository.FindAll(x => !x.IsDeleted, includeProperties: [x => x.PetCenterServices, x => x.Job])
            : petCenterRepository.FindAll(x => !x.IsDeleted && x.Application.Name.Contains(request.SearchTerm), includeProperties: [x => x.PetCenterServices, x => x.Job]);

        if (request.PetServiceId.HasValue)
            EventsQuery = EventsQuery.Where(x => x.PetCenterServices.Any(x => x.PetServiceId == request.PetServiceId));

        return EventsQuery;
    }

    //public static Expression<Func<Domain.Entities.PetCenter, object>> GetSortProperty(Query.GetPetCenterQuery request)
    //=> request.SortColumn?.ToLower() switch
    //{
    //    "name" => e => e.CreatedDate,
    //    _ => e => e.CreatedDate
    //};
}
