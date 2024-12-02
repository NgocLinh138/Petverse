using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Job;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Job.Commands;
public sealed class CreateJobCommandHandler : ICommandHandler<Command.CreateJobCommand, Responses.JobResponse>
{
    private readonly IJobRepository jobRepository;
    private readonly IPetCenterRepository petCenterRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ISpeciesJobRepository jobSpeciesRepository;
    private readonly IMapper mapper;
    private readonly IPetCenterServiceRepository petCenterServiceRepository;
    public CreateJobCommandHandler(
        IJobRepository JobRepository,
        IMapper mapper,
        IPetCenterRepository petCenterRepository,
        IUnitOfWork unitOfWork,
        ISpeciesJobRepository petTypeJobRepository,
        IPetCenterServiceRepository petCenterServiceRepository)

    {
        this.jobRepository = JobRepository;
        this.mapper = mapper;
        this.petCenterRepository = petCenterRepository;
        this.unitOfWork = unitOfWork;
        this.jobSpeciesRepository = petTypeJobRepository;
        this.petCenterServiceRepository = petCenterServiceRepository;
    }

    public async Task<Result<Responses.JobResponse>> Handle(Command.CreateJobCommand request, CancellationToken cancellationToken)
    {
        // CHeck PetCenter Exist?
        var petCenter = await petCenterRepository.FindByIdAsync(request.PetCenterId);
        if (petCenter == null)
            return Result.Failure<Responses.JobResponse>("Không tìm thấy trung tâm.", StatusCodes.Status404NotFound);

        // Check PetCenter Have Job?
        if (petCenter.Job != null)
            return Result.Failure<Responses.JobResponse>("Công việc đã tồn tại.", StatusCodes.Status400BadRequest);

        // Create Job
        var newJob = Domain.Entities.Job.Create(request);

        await jobRepository.AddAsync(newJob);
        await unitOfWork.SaveChangesAsync();

        // UpdatePrice
        await UpdatePetCenterServicePrice(request.PetCenterService);

        // Add PetSpeciesJob
        await AddSpeciesJobAsync(request.SpeciesIds, newJob.Id);

        var resultResponse = mapper.Map<Responses.JobResponse>(newJob);
        return Result.Success(resultResponse, 201);
    }

    private async Task UpdatePetCenterServicePrice(IEnumerable<Command.PetCenterServiceUpdatePrice> petCenterServices)
    {
        foreach (var item in petCenterServices)
        {
            var petCenterService = await petCenterServiceRepository.FindByIdAsync(item.Id);
            if (petCenterService == null)
                continue;
            petCenterService.Capacity = item.Capacity;
            petCenterService.Schedule = string.Join(";", item.Schedule.Select(s => $"{s.Time}-{s.Description}"));
            petCenterService.Price = item.Price;
            petCenterService.Type = item.Type;
        }
    }

    private async Task AddSpeciesJobAsync(IEnumerable<int> speciesIds, Guid jobId)
    {
        var speciesJobs = speciesIds
            .Select(speciesId => new Domain.Entities.JunctionEntity.SpeciesJob
            {
                JobId = jobId,
                SpeciesId = speciesId
            })
            .ToList();

        await jobSpeciesRepository.AddMultiAsync(speciesJobs);
    }
}
