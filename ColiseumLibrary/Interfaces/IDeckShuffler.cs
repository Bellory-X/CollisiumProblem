using ColiseumLibrary.Contracts.Cards;

namespace ColiseumLibrary.Interfaces;

public interface IDeckShuffler
{
    public Deck Shuffle(Card[] cards);
}