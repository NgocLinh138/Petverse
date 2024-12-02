using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IPhotoRepository : IRepositoryBase<PetImage, int>
    {
        Task<List<PetImage>> GetPhotosByPetIdAsync(int petId);
    }
}
