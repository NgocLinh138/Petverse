using Domain.Abstractions.Repositories;
using Domain.Entities.JunctionEntity;
using Persistence.Repositories.Base;

namespace Persistence.Repositories
{
    public class VaccineReccomendationRepository : RepositoryBase<VaccineRecommendation, int>, IVaccineReccomendationRepository
    {
        public VaccineReccomendationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
