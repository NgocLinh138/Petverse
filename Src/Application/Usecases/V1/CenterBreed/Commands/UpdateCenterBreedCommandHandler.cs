using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.CenterBreed;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.CenterBreed.Commands
{
    public sealed class UpdateCenterBreedCommandHandler : ICommandHandler<Command.UpdateCenterBreedCommand, Responses.CenterBreedResponse>
    {
        private readonly ICenterBreedRepository centerBreedRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateCenterBreedCommandHandler(
            ICenterBreedRepository centerBreedRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.centerBreedRepository = centerBreedRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.CenterBreedResponse>> Handle(Command.UpdateCenterBreedCommand request, CancellationToken cancellationToken)
        {
            var centerBreed = await centerBreedRepository.FindByIdAsync(request.Id, cancellationToken);
            if (centerBreed == null)
                return Result.Failure<Responses.CenterBreedResponse>("Không tìm thấy giống thú cưng.", StatusCodes.Status404NotFound);

            centerBreed.Update(request);
            centerBreedRepository.Update(centerBreed);
            await unitOfWork.SaveChangesAsync();

            var response = mapper.Map<Responses.CenterBreedResponse>(centerBreed);

            return Result.Success(response, 202);
        }
    }
}
