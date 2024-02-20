using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Orders;

namespace ColiseumLibrary.Services;

public class SimpleExperimentService(
    IDeckShuffler shuffler,
    ICardPickStrategy playerService, 
    ICardPickStrategy opponentService
    ) : IExperimentService
{
    public Card[] Cards { get; set; } = Deck.GetCards();
    public IDeckShuffler Shuffler { set => shuffler = value; }
    public bool Output { get; private set; }

    public Task Run(int id)
    {
        var deck = shuffler.Shuffle(Cards);
        var playerChoice = playerService.Pick(deck.FirstHalf.ToArray());
        var opponentChoice = opponentService.Pick(deck.FirstHalf.ToArray());
        
        Output = deck.FirstHalf[opponentChoice] == deck.SecondHalf[playerChoice];
        
        return Task.CompletedTask;
    }
}