using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Place;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Place.Commands
{
    public sealed class DeletePlaceCommandHandler : ICommandHandler<Command.DeletePlaceCommand>
    {
        private readonly IPlaceRepository placeRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeletePlaceCommandHandler(
            IPlaceRepository placeRepository,
            IUnitOfWork unitOfWork)
        {
            this.placeRepository = placeRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(Command.DeletePlaceCommand request, CancellationToken cancellationToken)
        {
            var place = await placeRepository.FindByIdAsync(request.Id, cancellationToken);
            if (place == null)
                return Result.Failure<Responses.PlaceResponse>("Không tìm thấy địa điểm.", StatusCodes.Status404NotFound);

            placeRepository.Remove(place);
            await unitOfWork.SaveChangesAsync();

            return Result.Success(202);
        }
    }
}
