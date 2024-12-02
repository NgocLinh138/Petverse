using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetService;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.PetService.Commands
{
    public sealed class UpdatePetServiceCommandHandler : ICommandHandler<Command.UpdatePetServiceCommand, Responses.PetServiceResponse>
    {
        private readonly IPetServiceRepository petServiceRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdatePetServiceCommandHandler(
            IPetServiceRepository petServiceRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.petServiceRepository = petServiceRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.PetServiceResponse>> Handle(Command.UpdatePetServiceCommand request, CancellationToken cancellationToken)
        {
            var petService = await petServiceRepository.FindByIdAsync(request.Id, cancellationToken);
            if (petService == null)
                return Result.Failure<Responses.PetServiceResponse>("Không tìm thấy dịch vụ.", StatusCodes.Status404NotFound);

            petService.Update(request);
            petServiceRepository.Update(petService);
            await unitOfWork.SaveChangesAsync();

            var response = mapper.Map<Responses.PetServiceResponse>(petService);

            return Result.Success(response, 202);

        }
    }
}