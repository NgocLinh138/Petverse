using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class SpeciesRepository : RepositoryBase<Species, int>, ISpeciesRepository
{
    private readonly ApplicationDbContext _context;

    public SpeciesRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<Vaccine>> GetVaccinesBySpeciesAsync(int speciesId)
    {
        return await _context.Vaccine
            .Where(x => x.SpeciesId == speciesId)
            .ToListAsync();
    }

    public async Task<List<int>> GetExistingSpeciesIdsAsync(List<int> speciesIds)
    {
        return await _context.Species
            .Where(x => speciesIds.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();
    }
}
