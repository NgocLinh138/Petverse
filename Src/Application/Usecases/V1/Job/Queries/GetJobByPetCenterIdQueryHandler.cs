using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Job;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;


namespace Application.Usecases.V1.Job.Queries;
public sealed class GetJobByPetCenterIdQueryHandler : IQueryHandler<Query.GetJobByPetCenterIdQuery, Responses.JobResponse>
{
    private readonly IJobRepository JobRepository;
    private readonly IMapper mapper;

    public GetJobByPetCenterIdQueryHandler(
        IJobRepository JobRepository,
        IMapper mapper)
    {
        this.JobRepository = JobRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.JobResponse>> Handle(Query.GetJobByPetCenterIdQuery request, CancellationToken cancellationToken)
    {
        var result = await JobRepository.FindSingleAsync(x => x.PetCenterId == request.PetCenterId);
        if (result == null)
            return Result.Failure<Responses.JobResponse>("Không tìm thấy công việc.", StatusCodes.Status404NotFound);

        var resultResponse = mapper.Map<Responses.JobResponse>(result);
        return Result.Success(resultResponse);
    }
}
