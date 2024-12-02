using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class BreedRepository : RepositoryBase<Breed, int>, IBreedRepository
{
    public BreedRepository(ApplicationDbContext context) : base(context)
    {
    }
}
