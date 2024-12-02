using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetCenterService;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.PetSitterService.Commands
{
    public sealed class DeletePetCenterServiceCommandHandler : ICommandHandler<Command.DeletePetCenterServiceCommand>
    {
        private readonly IPetCenterServiceRepository petCenterServiceRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeletePetCenterServiceCommandHandler(
            IPetCenterServiceRepository petCenterServiceRepository,
            IUnitOfWork unitOfWork)
        {
            this.petCenterServiceRepository = petCenterServiceRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(Command.DeletePetCenterServiceCommand request, CancellationToken cancellationToken)
        {
            var petCenterService = await petCenterServiceRepository.FindByIdAsync(request.Id, cancellationToken);
            if (petCenterService == null)
                return Result.Failure("Không tìm thấy dịch vụ trung tâm thú cưng.", StatusCodes.Status404NotFound);

            petCenterServiceRepository.Remove(petCenterService);
            await unitOfWork.SaveChangesAsync();

            return Result.Success(StatusCodes.Status204NoContent);
        }
    }
}
