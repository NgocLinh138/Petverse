using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories
{
    public class TrackingRepository : RepositoryBase<Tracking, int>, ITrackingRepository
    {
        public TrackingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
