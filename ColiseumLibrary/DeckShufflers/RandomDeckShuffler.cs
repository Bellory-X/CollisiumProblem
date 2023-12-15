using System.Collections.Immutable;
using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Interfaces;

namespace ColiseumLibrary.DeckShufflers;

public class RandomDeckShuffler : IDeckShuffler
{
    public Deck Shuffle(Card[] cards)
    {
        ArgumentNullException.ThrowIfNull(cards);
        var random = new Random();
        for (var i = 0; i < cards.Length; i++)
        {
            var j = random.Next(cards.Length);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
        return new Deck { Cards = cards.ToImmutableArray()};
    }
}