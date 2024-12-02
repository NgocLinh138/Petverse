using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IPetVaccinatedRepository : IRepositoryBase<PetVaccinated, int>
{
}
