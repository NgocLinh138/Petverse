using Domain.Abstractions;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.VaccineRecommendation.Commands
{
    public sealed class UpdateVaccineRecommendationCommandHandler
    {
        private readonly IPetRepository petRepository;
        private readonly ISpeciesRepository speciesRepository;
        private readonly IVaccineReccomendationRepository vaccineReccomendationRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateVaccineRecommendationCommandHandler(
            IPetRepository petRepository,
            ISpeciesRepository speciesRepository,
            IVaccineReccomendationRepository vaccineReccomendationRepository,
            IUnitOfWork unitOfWork)
        {
            this.petRepository = petRepository;
            this.speciesRepository = speciesRepository;
            this.vaccineReccomendationRepository = vaccineReccomendationRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task UpdateVaccineRecommendationsAsync(int petId)
        {
            var pet = await petRepository.FindByIdAsync(petId);
            if (pet == null) return;

            var species = await speciesRepository.FindByIdAsync(pet.Breed.SpeciesId);
            if (species == null) return;

            var vaccines = await speciesRepository.GetVaccinesBySpeciesAsync(species.Id);
            foreach (var vaccine in vaccines)
            {
                var existingRecommendation = await vaccineReccomendationRepository.FindSingleAsync(r => r.PetId == petId && r.VaccineId == vaccine.Id);

                var ageInMonths = (DateTime.Now - pet.BirthDate).Days / 30;
                var vaccineAgeRequirement = vaccine.MinAge;

                if (existingRecommendation == null && ageInMonths >= vaccineAgeRequirement)
                {
                    var newRecommendation = new Domain.Entities.JunctionEntity.VaccineRecommendation
                    {
                        PetId = petId,
                        VaccineId = vaccine.Id,
                    };
                    await vaccineReccomendationRepository.AddAsync(newRecommendation);
                    await unitOfWork.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateVaccineRecommendationsForAllPets()
        {
            var pets = petRepository.FindAll();
            foreach (var pet in pets)
            {
                await UpdateVaccineRecommendationsAsync(pet.Id);
            }
        }
    }
}
