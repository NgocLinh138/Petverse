using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetService;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.PetService.Commands
{
    public sealed class DeletePetServiceCommandHandler : ICommandHandler<Command.DeletePetServiceCommand>
    {
        private readonly IPetServiceRepository petServiceRepository;

        public DeletePetServiceCommandHandler(IPetServiceRepository petServiceRepository)
        {
            this.petServiceRepository = petServiceRepository;
        }

        public async Task<Result> Handle(Command.DeletePetServiceCommand request, CancellationToken cancellationToken)
        {
            var petService = await petServiceRepository.FindByIdAsync(request.Id, cancellationToken);
            if (petService == null)
                return Result.Failure("Không tìm thấy dịch vụ.", StatusCodes.Status404NotFound);

            petServiceRepository.Remove(petService);

            return Result.Success(202);

        }
    }
}
