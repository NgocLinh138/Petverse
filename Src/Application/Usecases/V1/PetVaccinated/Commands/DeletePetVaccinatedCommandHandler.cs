using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetVaccinated;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.PetVaccinated.Commands
{
    public sealed class DeletePetVaccinatedCommandHandler : ICommandHandler<Command.DeletePetVaccinatedCommand>
    {
        private readonly IPetVaccinatedRepository petVaccinatedRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeletePetVaccinatedCommandHandler(
            IPetVaccinatedRepository petVaccinatedRepository,
            IUnitOfWork unitOfWork)
        {
            this.petVaccinatedRepository = petVaccinatedRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(Command.DeletePetVaccinatedCommand request, CancellationToken cancellationToken)
        {
            var petVaccinated = await petVaccinatedRepository.FindByIdAsync(request.Id, cancellationToken); 
            if (petVaccinated == null)
                return Result.Failure<Responses.PetVaccinatedResponse>("Không tìm thấy thông tin đã tiêm phòng của thú cưng.", StatusCodes.Status404NotFound);

            petVaccinatedRepository.Remove(petVaccinated);
            await unitOfWork.SaveChangesAsync();

            return Result.Success(202);
        }
    }
}
