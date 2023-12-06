using ColiseumLibrary.Contracts.Cards;
using GodsApi.Models;

namespace GodsApi.Services;

public interface IWorker
{
    public Task<bool> RunExperiment(Card[] playerCards, Card[] opponentCards);
}