using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;

namespace Persistence.Repositories
{
    public class PhotoRepository : RepositoryBase<PetImage, int>, IPhotoRepository
    {
        private readonly ApplicationDbContext _context;

        public PhotoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PetImage>> GetPhotosByPetIdAsync(int petId)
        {
            return await _context.Set<PetImage>()
                  .Where(photo => photo.PetId == petId)
                  .ToListAsync();
        }
    }
}
