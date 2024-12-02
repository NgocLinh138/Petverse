using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Species;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;
using Persistence.Repositories;

namespace Application.Usecases.V1.Breed.Commands;
public sealed class CreateBreedCommandHandler : ICommandHandler<Command.CreateBreedCommand, Responses.BreedResponse>
{
    private readonly ISpeciesRepository SpeciesRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IBreedRepository BreedRepository;
    private readonly IMapper mapper;

    public CreateBreedCommandHandler(
        IBreedRepository BreedRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ISpeciesRepository SpeciesRepository)

    {
        this.BreedRepository = BreedRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.SpeciesRepository = SpeciesRepository;
    }

    public async Task<Result<Responses.BreedResponse>> Handle(Command.CreateBreedCommand request, CancellationToken cancellationToken)
    {
        var species = await SpeciesRepository.FindByIdAsync(request.SpeciesId.Value);
        if (species == null)
            return Result.Failure<Responses.BreedResponse>("Không tìm thấy loại thú cưng.", StatusCodes.Status404NotFound);

        var existingBreed = await BreedRepository.FindSingleAsync(x => x.Name == request.Name && x.SpeciesId == request.SpeciesId, cancellationToken);
        if (existingBreed != null)
            return Result.Failure<Responses.BreedResponse>("Tên của giống thú cưng đã tồn tại.", StatusCodes.Status409Conflict);

        var newBreed = new Domain.Entities.Breed
        {
            SpeciesId = request.SpeciesId.Value,
            Name = request.Name,
            Description = request.Description,
        };

        await BreedRepository.AddAsync(newBreed);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var resultResponse = mapper.Map<Responses.BreedResponse>(newBreed);
        return Result.Success(resultResponse, 201);

    }
}
