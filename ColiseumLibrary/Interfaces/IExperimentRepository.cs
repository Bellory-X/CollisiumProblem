using System.Collections.Immutable;
using ColiseumLibrary.Model.Experiments;

namespace ColiseumLibrary.Interfaces;

public interface IExperimentRepository
{
    bool AddExperiment(Experiment domainModel);

    bool AddExperiments(ImmutableList<Experiment> domainModels);

    Experiment? GetExperimentById(int id);
    
    List<Experiment> GetExperiments(int count);
}