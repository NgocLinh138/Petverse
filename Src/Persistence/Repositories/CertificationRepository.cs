using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories
{
    public class CertificationRepository : RepositoryBase<Certification, int>, ICertificationRepository
    {
        public CertificationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
