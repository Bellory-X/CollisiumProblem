using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.Strategies;

namespace ColiseumLibrary.Contracts.ExperimentWorkers;

public class SimpleExperimentWorker(
    ICardPickStrategy playerStrategy, 
    ICardPickStrategy opponentStrategy
    ) : IExperimentWorker
{
    public Task<bool> RunExperiment(Deck deck)
    {
        var playerChoice = playerStrategy.Pick(deck.FirstHalf.ToArray());
        var opponentChoice = opponentStrategy.Pick(deck.SecondHalf.ToArray());

        return Task.Run(() => deck.FirstHalf[opponentChoice] == deck.SecondHalf[playerChoice]);
    }
}