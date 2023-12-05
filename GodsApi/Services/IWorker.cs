using ColiseumLibrary.Contracts.Cards;
using GodsApi.Models;

namespace GodsApi.Services;

public interface IWorker
{
    public Task<Experiment> RunExperiment(int ordinal, Card[] firstDeck, Card[] secondDeck);
}