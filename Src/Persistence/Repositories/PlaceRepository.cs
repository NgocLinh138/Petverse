using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories
{
    public class PlaceRepository : RepositoryBase<Place, int>, IPlaceRepository
    {
        public PlaceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
