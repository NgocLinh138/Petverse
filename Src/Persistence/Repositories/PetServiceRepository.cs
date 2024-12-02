using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories
{
    public class PetServiceRepository : RepositoryBase<PetService, int>, IPetServiceRepository
    {
        public PetServiceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
