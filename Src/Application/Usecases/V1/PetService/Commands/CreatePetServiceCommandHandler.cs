using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetService;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.PetService.Commands
{
    public sealed class CreatePetServiceCommandHandler : ICommandHandler<Command.CreatePetServiceCommand, Responses.PetServiceResponse>
    {
        private readonly IPetServiceRepository petServiceRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreatePetServiceCommandHandler(IPetServiceRepository petServiceRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.petServiceRepository = petServiceRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.PetServiceResponse>> Handle(Command.CreatePetServiceCommand request, CancellationToken cancellationToken)
        {
            var existingPetService = await petServiceRepository.FindSingleAsync(x => x.Name == request.Name);
            if (existingPetService != null)
                return Result.Failure<Responses.PetServiceResponse>("Tên dịch vụ đã tồn tại.");

            var petService = new Domain.Entities.PetService
            {
                Name = request.Name,
                Description = request.Description
            };

            await petServiceRepository.AddAsync(petService);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var response = mapper.Map<Responses.PetServiceResponse>(petService);
            return Result.Success(response, 201);
        }
    }

}
