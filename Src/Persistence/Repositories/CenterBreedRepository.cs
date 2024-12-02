using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class CenterBreedRepository : RepositoryBase<CenterBreed, int>, ICenterBreedRepository
{
    public CenterBreedRepository(ApplicationDbContext context) : base(context)
    {
    }
}
