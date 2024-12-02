using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetCenterService;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.PetSitterService.Commands
{
    public sealed class UpdatePetCenterServiceCommandHandler : ICommandHandler<Command.UpdatePetCenterServiceCommand, Responses.PetCenterServiceResponse>
    {
        private readonly IPetCenterServiceRepository petCenterServiceRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdatePetCenterServiceCommandHandler(
            IPetCenterServiceRepository petCenterServiceRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.petCenterServiceRepository = petCenterServiceRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.PetCenterServiceResponse>> Handle(Command.UpdatePetCenterServiceCommand request, CancellationToken cancellationToken)
        {
            var petCenterService = await petCenterServiceRepository.FindByIdAsync(request.Id, cancellationToken);
            if (petCenterService == null)
                return Result.Failure<Responses.PetCenterServiceResponse>("Không tìm thấy dịch vụ trung tâm thú cưng.", StatusCodes.Status404NotFound);

            petCenterService.Update(request);
            petCenterServiceRepository.Update(petCenterService);
            await unitOfWork.SaveChangesAsync();

            var response = mapper.Map<Responses.PetCenterServiceResponse>(petCenterService);
            return Result.Success(response, 202);

        }
    }
}
