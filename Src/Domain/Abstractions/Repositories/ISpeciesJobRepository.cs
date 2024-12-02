using Domain.Entities.JunctionEntity;

namespace Domain.Abstractions.Repositories;

public interface ISpeciesJobRepository
{
    Task AddMultiAsync(IEnumerable<SpeciesJob> petTypeJobs);
    void RemoveMulti(IEnumerable<SpeciesJob> petTypeJobs);
    IEnumerable<string> GetSpeciesNameByJobId(Guid JobId);
}
