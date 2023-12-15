using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Interfaces;

namespace ColiseumLibrary.Workers;

public class SimpleWorker(ICardPickStrategy playerStrategy, ICardPickStrategy opponentStrategy)
{
    public bool Work(Deck deck)
    {
        var playerChoice = playerStrategy.Pick(deck.FirstHalf.ToArray());
        var opponentChoice = opponentStrategy.Pick(deck.SecondHalf.ToArray());

        return deck.FirstHalf[opponentChoice] == deck.SecondHalf[playerChoice];
    }
}