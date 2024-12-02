using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;

namespace Persistence.Repositories
{
    public class VaccineRepository : RepositoryBase<Vaccine, int>, IVaccineRepository
    {
        private readonly ApplicationDbContext _context;

        public VaccineRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Vaccine?> FindByNameAndSpeciesIdAsync(string name, int speciesId, CancellationToken cancellationToken)
        {
            return await _context.Vaccine
                .Where(x => x.Name == name && x.SpeciesId == speciesId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
