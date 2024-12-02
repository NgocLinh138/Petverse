using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Pet;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Pet.Commands
{
    public sealed class DeletePetCommandHandler : ICommandHandler<Command.DeletePetCommand>
    {
        private readonly IPetRepository petRepository;
        private readonly IVaccineReccomendationRepository vaccineReccomendationRepository;

        public DeletePetCommandHandler(
            IPetRepository petRepository,
            IVaccineReccomendationRepository vaccineReccomendationRepository)
        {
            this.petRepository = petRepository;
            this.vaccineReccomendationRepository = vaccineReccomendationRepository;
        }

        public async Task<Result> Handle(Command.DeletePetCommand request, CancellationToken cancellationToken)
        {
            var pet = await petRepository.FindByIdAsync(request.Id, cancellationToken);
            if (pet == null)
                return Result.Failure("Không tìm thấy thú cưng.", StatusCodes.Status404NotFound);

            var vaccineRecommendations = vaccineReccomendationRepository.FindAll(r => r.PetId == pet.Id);
            foreach (var vaccineRecommendation in vaccineRecommendations)
            {
                vaccineReccomendationRepository.Remove(vaccineRecommendation);
            }

            petRepository.SoftDelete(pet);

            return Result.Success(202);
        }
    }
}
