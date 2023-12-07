using System.Collections.Immutable;
using ColiseumLibrary.Contracts.Cards;

namespace ColiseumLibrary.Contracts.DeckShufflers;

public class RandomDeckShuffler : IDeckShuffler
{
    public Deck Shuffle(Card[] cards)
    {
        if (cards == null) throw new ArgumentNullException(nameof(cards));
        var random = new Random();
        for (var i = 0; i < cards.Length; i++)
        {
            var j = random.Next(cards.Length);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
        return new Deck(cards.ToImmutableArray());
    }
}