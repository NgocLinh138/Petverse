using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Vaccine;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Vaccine.Commands
{
    public sealed class DeleteVaccineCommandHandler : ICommandHandler<Command.DeleteVaccineCommand>
    {
        private readonly IVaccineRepository vaccineRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteVaccineCommandHandler(
            IVaccineRepository vaccineRepository,
            IUnitOfWork unitOfWork)
        {
            this.vaccineRepository = vaccineRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(Command.DeleteVaccineCommand request, CancellationToken cancellationToken)
        {
            var vaccine = await vaccineRepository.FindByIdAsync(request.Id, cancellationToken);
            if (vaccine == null)
                return Result.Failure<Responses.VaccineResponse>("Không tìm thấy vaccine.", StatusCodes.Status404NotFound);

            vaccineRepository.Remove(vaccine);
            await unitOfWork.SaveChangesAsync();

            return Result.Success(202);
        }
    }
}
