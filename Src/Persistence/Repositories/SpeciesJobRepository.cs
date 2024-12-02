using Domain.Abstractions.Repositories;
using Domain.Entities.JunctionEntity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class SpeciesJobRepository : ISpeciesJobRepository
{
    private readonly ApplicationDbContext context;

    public SpeciesJobRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<string> GetSpeciesNameByJobId(Guid JobId)
    {
        var speciesJob = context.SpeciesJob
                .Where(x => x.JobId == JobId)
                .AsSplitQuery()
                .Include(x => x.Species)
                .Select(x => x.Species.Name)
                .AsEnumerable();

        return speciesJob;
    }

    public async Task AddMultiAsync(IEnumerable<SpeciesJob> jobSpecies)
    {
        foreach (var item in jobSpecies)
            await context.SpeciesJob.AddAsync(item);
    }

    public void RemoveMulti(IEnumerable<SpeciesJob> jobSpecies)
        => context.SpeciesJob.RemoveRange(jobSpecies);
}
