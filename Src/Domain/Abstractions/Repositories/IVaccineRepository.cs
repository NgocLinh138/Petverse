using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IVaccineRepository : IRepositoryBase<Vaccine, int>
    {
        Task<Vaccine?> FindByNameAndSpeciesIdAsync(string name, int speciesId, CancellationToken cancellationToken);
    }
}
