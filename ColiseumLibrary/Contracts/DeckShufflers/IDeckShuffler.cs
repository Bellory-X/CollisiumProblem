using ColiseumLibrary.Contracts.Cards;

namespace ColiseumLibrary.Contracts.DeckShufflers;

public interface IDeckShuffler
{
    public Deck Shuffle(Card[] cards);
}