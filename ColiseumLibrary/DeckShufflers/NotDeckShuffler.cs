using System.Collections.Immutable;
using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;

namespace ColiseumLibrary.DeckShufflers;

public class NotDeckShuffler : IDeckShuffler
{
    public Deck Shuffle(Card[] cards)
    {
        ArgumentNullException.ThrowIfNull(cards);
        return new Deck { Cards = cards.ToImmutableArray()};
    }
}