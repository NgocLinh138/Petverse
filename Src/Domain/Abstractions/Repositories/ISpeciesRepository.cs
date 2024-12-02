using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface ISpeciesRepository : IRepositoryBase<Species, int>
{
    Task<List<Vaccine>> GetVaccinesBySpeciesAsync(int speciesId);
    Task<List<int>> GetExistingSpeciesIdsAsync(List<int> speciesIds);
}
