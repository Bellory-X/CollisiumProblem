using ColiseumLibrary.Contracts.Cards;
using GodsApi.Models;

namespace ColiseumLibrary.Contracts.ExperimentWorkers;

public interface IExperimentWorker
{
    public Task<bool> RunExperiment(Deck deck);
}