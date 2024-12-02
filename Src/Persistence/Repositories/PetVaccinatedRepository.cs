using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class PetVaccinatedRepository : RepositoryBase<PetVaccinated, int>, IPetVaccinatedRepository
{
    public PetVaccinatedRepository(ApplicationDbContext context) : base(context)
    {
    }
}
