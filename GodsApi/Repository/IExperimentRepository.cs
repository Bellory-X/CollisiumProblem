using System.Collections.Immutable;
using GodsApi.Models;

namespace GodsApi.Repository;

public interface IExperimentRepository
{
    bool AddExperiments(ImmutableList<Experiment> domainModels);

    bool DeleteAllExperiments(ImmutableList<Experiment> domainModels);
    
    List<Experiment> GetLatestExperiments(int count);
}