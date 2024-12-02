using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Vaccine;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Vaccine.Commands
{
    public sealed class CreateVaccineCommandHandler : ICommandHandler<Command.CreateVaccineCommand, Responses.VaccineResponse>
    {
        private readonly IVaccineRepository vaccineRepository;
        private readonly ISpeciesRepository speciesRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateVaccineCommandHandler(
            IVaccineRepository vaccineRepository,
            ISpeciesRepository speciesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.vaccineRepository = vaccineRepository;
            this.speciesRepository = speciesRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.VaccineResponse>> Handle(Command.CreateVaccineCommand request, CancellationToken cancellationToken)
        {
            var species = await speciesRepository.FindByIdAsync(request.SpeciesId, cancellationToken);
            if (species == null)
                return Result.Failure<Responses.VaccineResponse>("Không tìm thấy loại thú cưng.", StatusCodes.Status404NotFound);

            var existingVaccine = await vaccineRepository.FindSingleAsync(x => x.Name == request.Name);
            if (existingVaccine != null)
                return Result.Failure<Responses.VaccineResponse>("Tên vaccine đã tồn tại.", StatusCodes.Status400BadRequest);

            var vaccine = new Domain.Entities.Vaccine
            {
                SpeciesId = request.SpeciesId,
                Name = request.Name,
                Description = request.Description,
                MinAge = request.MinAge
            };

            await vaccineRepository.AddAsync(vaccine);
            await unitOfWork.SaveChangesAsync();

            var response = mapper.Map<Responses.VaccineResponse>(vaccine);

            return Result.Success(response, 201);
        }
    }
}
