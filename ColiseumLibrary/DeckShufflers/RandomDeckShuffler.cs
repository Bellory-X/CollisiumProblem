using System.Collections.Immutable;
using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;

namespace ColiseumLibrary.DeckShufflers;

public class RandomDeckShuffler : IDeckShuffler
{
    private readonly Random _random = new();
    public Deck Shuffle(Card[] cards)
    {
        ArgumentNullException.ThrowIfNull(cards);
        for (var i = 0; i < cards.Length; i++)
        {
            var j = _random.Next(cards.Length);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
        return new Deck(cards.ToImmutableArray());
    }
}