using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetCenter;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;
using static Contract.Services.V1.Job.Responses;
using static Contract.Services.V1.PetCenter.Responses;


namespace Application.Usecases.V1.PetCenter.Queries;
public sealed class GetPetCenterByIdQueryHandler : IQueryHandler<Query.GetPetCenterByIdQuery, Responses.PetCenterGetByIdResponse>
{
    private readonly IPetCenterRepository petCenterRepository;
    private readonly IApplicationRepository applicationRepository;
    private readonly IAppointmentRateRepository petCenterRateRepository;
    private readonly ISpeciesJobRepository speciesJobRepository;
    private readonly IPetCenterServiceRepository petCenterServiceRepository;
    private readonly IMapper mapper;

    public GetPetCenterByIdQueryHandler(
        IPetCenterRepository petCenterRepository,
        IMapper mapper,
        IApplicationRepository applicationRepository,
        IAppointmentRateRepository petCenterRateRepository,
        ISpeciesJobRepository speciesJobRepository,
        IPetCenterServiceRepository petCenterServiceRepository)
    {
        this.petCenterRepository = petCenterRepository;
        this.mapper = mapper;
        this.applicationRepository = applicationRepository;
        this.petCenterRateRepository = petCenterRateRepository;
        this.speciesJobRepository = speciesJobRepository;
        this.petCenterServiceRepository = petCenterServiceRepository;
    }

    public async Task<Result<PetCenterGetByIdResponse>> Handle(Query.GetPetCenterByIdQuery request, CancellationToken cancellationToken)
    {
        var petCenter = await petCenterRepository.FindByIdAsync(request.Id);
        if (petCenter == null)
            return Result.Failure<Responses.PetCenterGetByIdResponse>("Không tìm thấy trung tâm.", StatusCodes.Status404NotFound);

        var application = await applicationRepository.FindByIdAsync(petCenter.ApplicationId, cancellationToken);
        if (application == null)
            return Result.Failure<PetCenterGetByIdResponse>("Không tìm thấy đơn đăng ký.", StatusCodes.Status400BadRequest);

        var petCenterServiceResponses = await petCenterServiceRepository.GetPetCenterServiceResponsesByCenterIdAsync(request.Id);


        var response = new PetCenterGetByIdResponse
        {
            Id = petCenter.Id,
            ApplicationId = petCenter.ApplicationId,
            Name = petCenter.Application.Name,
            NumPet = petCenter.NumPet,
            IsVerified = petCenter.IsVerified,
            PhoneNumber = petCenter.Application.PhoneNumber,
            Address = petCenter.Application.Address,
            Avatar = petCenter.Application.Avatar,
            Description = petCenter.Application.Description,
            Yoe = petCenter.Application.Yoe,
            Rate = petCenterServiceResponses?.Average(x => x.Rate) ?? 0,
            Species = petCenter.Job is null ? null : speciesJobRepository.GetSpeciesNameByJobId(petCenter.Job.Id),
            PetCenterServices = petCenterServiceResponses,
            Certifications = mapper.Map<ICollection<CertificationResponse>>(application.Certifications),
            Job = mapper.Map<JobResponse>(petCenter.Job),
        };

        return Result.Success(response);
    }
}
