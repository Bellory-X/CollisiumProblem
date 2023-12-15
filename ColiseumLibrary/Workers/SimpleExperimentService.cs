using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;

namespace ColiseumLibrary.Workers;

public class SimpleExperimentService(
    IDeckShuffler shuffler,
    ICardPickStrategy playerStrategy, 
    ICardPickStrategy opponentStrategy
    ) : IExperimentService
{
    private bool _output;
    public Card[] Cards { get; set; } = Deck.GetCards();
    public bool Output {
        get
        {
            ArgumentNullException.ThrowIfNull(_output);
            return _output;
        }
    }
    public IDeckShuffler Shuffler { get => shuffler; set => shuffler = value; }

    public void Run()
    {
        var deck = shuffler.Shuffle(Cards);
        var playerChoice = playerStrategy.Pick(deck.FirstHalf.ToArray());
        var opponentChoice = opponentStrategy.Pick(deck.SecondHalf.ToArray());

        _output = deck.FirstHalf[opponentChoice] == deck.SecondHalf[playerChoice];
    }
}