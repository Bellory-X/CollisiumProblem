using ColiseumLibrary.Model.Cards;

namespace ColiseumLibrary.Interfaces;

public interface IDeckShuffler
{
    public Deck Shuffle(Card[] cards);
}