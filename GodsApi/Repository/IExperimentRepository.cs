using System.Collections.Immutable;
using GodsApi.Model;

namespace GodsApi.Repository;

public interface IExperimentRepository
{
    bool AddExperiment(Experiment domainModel);

    bool AddExperiments(ImmutableList<Experiment> domainModels);

    Experiment? GetExperimentById(int id);
    
    List<Experiment> GetExperiments(int count);
}