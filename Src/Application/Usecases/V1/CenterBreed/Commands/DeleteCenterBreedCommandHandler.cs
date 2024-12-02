using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.CenterBreed;
using Domain.Abstractions.Repositories;
using Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.CenterBreed.Commands
{
    public sealed class DeleteCenterBreedCommandHandler : ICommandHandler<Command.DeleteCenterBreedCommand>
    {
        private readonly ICenterBreedRepository centerBreedRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteCenterBreedCommandHandler(
            ICenterBreedRepository centerBreedRepository,
            IUnitOfWork unitOfWork)
        {
            this.centerBreedRepository = centerBreedRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(Command.DeleteCenterBreedCommand request, CancellationToken cancellationToken)
        {
            var centerBreed = await centerBreedRepository.FindByIdAsync(request.Id, cancellationToken);
            if (centerBreed == null)
                return Result.Failure<Responses.CenterBreedResponse>("Không tìm thấy giống thú cưng.", StatusCodes.Status404NotFound);

            centerBreedRepository.Remove(centerBreed);
            await unitOfWork.SaveChangesAsync();

            return Result.Success(202);
        }
    }
}
