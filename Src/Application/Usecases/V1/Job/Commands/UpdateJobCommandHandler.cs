using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Job;
using Domain.Abstractions.Repositories;
using Domain.Entities.JunctionEntity;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Job.Commands;
public sealed class UpdateJobCommandHandler : ICommandHandler<Command.UpdateJobCommand, Responses.JobResponse>
{
    private readonly IJobRepository JobRepository;
    private readonly ISpeciesJobRepository SpeciesJobRepository;
    private readonly IMapper mapper;

    public UpdateJobCommandHandler(
        IJobRepository JobRepository,
        IMapper mapper,
        ISpeciesJobRepository SpeciesJobRepository)
    {
        this.JobRepository = JobRepository;
        this.mapper = mapper;
        this.SpeciesJobRepository = SpeciesJobRepository;
    }
    public async Task<Result<Responses.JobResponse>> Handle(Command.UpdateJobCommand request, CancellationToken cancellationToken)
    {

        var Job = await JobRepository.FindByIdAsync(request.Id.Value);
        if (Job == null)
            return Result.Failure<Responses.JobResponse>("Không tìm thấy công việc.", StatusCodes.Status404NotFound);

        // Update Job
        Job.Update(request);
        JobRepository.Update(Job);

        // Update SpeciesJob
        UpdateSpeciesJob(request.SpeciesIds, Job);

        var resultResponse = mapper.Map<Responses.JobResponse>(Job);
        return Result.Success(resultResponse, 202);
    }

    private async Task UpdateSpeciesJob(IEnumerable<int>? SpeciesIds, Domain.Entities.Job job)
    {
        if (!SpeciesIds.Any())
            return;

        RemoveOldSpeciesJob(job.SpeciesJobs);
        await AddNewSpeciesJobAsync(SpeciesIds, job.Id);
    }
    private void RemoveOldSpeciesJob(ICollection<SpeciesJob> SpeciesJobs)
        => SpeciesJobRepository.RemoveMulti(SpeciesJobs);
    private async Task AddNewSpeciesJobAsync(IEnumerable<int> SpeciesIds, Guid jobId)
    {
        ICollection<SpeciesJob> SpeciesJobs = new List<SpeciesJob>();
        foreach (var SpeciesId in SpeciesIds)
        {
            var SpeciesJob = new SpeciesJob
            {
                JobId = jobId,
                SpeciesId = SpeciesId
            };
            SpeciesJobs.Add(SpeciesJob);
        }

        await SpeciesJobRepository.AddMultiAsync(SpeciesJobs);
    }
}
