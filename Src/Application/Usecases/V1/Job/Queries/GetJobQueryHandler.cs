using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Job;
using Domain.Abstractions.Repositories;
using Microsoft.IdentityModel.Tokens;


namespace Application.Usecases.V1.Job.Queries;
public sealed class GetJobQueryHandler : IQueryHandler<Query.GetJobQuery, PagedResult<Responses.JobResponse>>
{
    private readonly IApplicationRepository applicationRepository;
    private readonly IMapper mapper;

    public GetJobQueryHandler(
        IMapper mapper,
        IApplicationRepository applicationRepository)
    {
        this.mapper = mapper;
        this.applicationRepository = applicationRepository;
    }

    public async Task<Result<PagedResult<Responses.JobResponse>>> Handle(Query.GetJobQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = GetJobsQuery(request);

            var Events = await PagedResult<Domain.Entities.Job>.CreateAsync(
                query,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.JobResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private IQueryable<Domain.Entities.Job> GetJobsQuery(Query.GetJobQuery request)
    {
        var query = request.SearchTerm.IsNullOrEmpty()
            ? applicationRepository.FindAll()
            : applicationRepository.FindAll(predicate: x => x.Name.Contains(request.SearchTerm));


        if (request.PetServiceId.HasValue)
            query = query.Where(x => x.PetCenter.PetCenterServices.Any(x => x.PetServiceId == request.PetServiceId.Value));

        return query.Select(x => x.PetCenter.Job).Where(x => x.PetCenter.Job != null);
    }
}
