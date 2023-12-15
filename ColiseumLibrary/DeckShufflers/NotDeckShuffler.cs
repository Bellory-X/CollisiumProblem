using System.Collections.Immutable;
using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Interfaces;

namespace ColiseumLibrary.DeckShufflers;

public class NotDeckShuffler : IDeckShuffler
{
    public Deck Shuffle(Card[] cards)
    {
        ArgumentNullException.ThrowIfNull(cards);
        return new Deck { Cards = cards.ToImmutableArray()};
    }
}