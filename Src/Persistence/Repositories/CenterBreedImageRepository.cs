using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories
{
    public class CenterBreedImageRepository : RepositoryBase<CenterBreedImage, int>, ICenterBreedImageRepository
    {
        public CenterBreedImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
