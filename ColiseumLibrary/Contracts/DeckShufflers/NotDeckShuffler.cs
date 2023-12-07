using System.Collections.Immutable;
using ColiseumLibrary.Contracts.Cards;

namespace ColiseumLibrary.Contracts.DeckShufflers;

public class NotDeckShuffler : IDeckShuffler
{
    public Deck Shuffle(Card[] cards)
    {
        if (cards == null) throw new ArgumentNullException(nameof(cards));
        return new Deck(cards.ToImmutableArray());
    }
}