using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Vaccine;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Vaccine.Commands
{
    public sealed class UpdateVaccineCommandHandler : ICommandHandler<Command.UpdateVaccineCommand, Responses.VaccineResponse>
    {
        private readonly IVaccineRepository vaccineRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateVaccineCommandHandler(
            IVaccineRepository vaccineRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.vaccineRepository = vaccineRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.VaccineResponse>> Handle(Command.UpdateVaccineCommand request, CancellationToken cancellationToken)
        {
            var vaccine = await vaccineRepository.FindByIdAsync(request.Id, cancellationToken);
            if (vaccine == null)
                return Result.Failure<Responses.VaccineResponse>("Không tìm thấy vaccine.", StatusCodes.Status404NotFound);

            var existingVaccine = await vaccineRepository.FindSingleAsync(x => x.Name == request.Name && x.Id != request.Id);
            if (existingVaccine != null)
                return Result.Failure<Responses.VaccineResponse>("Tên vaccine đã tồn tại.", StatusCodes.Status400BadRequest);

            vaccine.Update(request);
            vaccineRepository.Update(vaccine);
            await unitOfWork.SaveChangesAsync();

            var response = mapper.Map<Responses.VaccineResponse>(vaccine);

            return Result.Success(response, 202);
        }
    }
}
