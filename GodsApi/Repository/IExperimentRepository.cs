using System.Collections.Immutable;
using GodsApi.Models;

namespace GodsApi.Repository;

public interface IExperimentRepository
{
    bool AddExperiments(ImmutableList<Experiment> domainModels);

    bool DeleteAllExperiments(ImmutableList<Experiment> domainModels);
    
    ICollection<Experiment> GetLatestExperiments(int count);
}