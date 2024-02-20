using System.Collections.Immutable;

namespace ColiseumLibrary.Model.Cards;

/// <summary>
/// Колода карт
/// </summary>
public record Deck
{
    public const int CardCount = 36;
    public ImmutableArray<Card> Cards { get; }
    public ImmutableArray<Card> FirstHalf { get => Cards.Take(CardCount / 2).ToImmutableArray(); }
    public ImmutableArray<Card> SecondHalf{ get => Cards.TakeLast(CardCount / 2).ToImmutableArray(); }

    public Deck(ImmutableArray<Card> cards)
    {
        ValidateCards(cards);
        Cards = cards;
    }
    
    public static Card[] GetCards()
    {
        var cards = new Card[CardCount];
        for (var i = 0; i < CardCount / 2; i++)
        {
            cards[i] = new Card(CardColor.Black);
            cards[CardCount - 1 - i] = new Card(CardColor.Red);
        }
        return cards;
    }

    private static void ValidateCards(ImmutableArray<Card> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);
        if (cards.Length != CardCount)
        {
            throw new ArgumentException("firstHalf and secondHalf must be equals 32");
        }
        var isRedCardCountValid = cards.Select(x => x.Color)
            .Count(x => x == CardColor.Red).Equals(CardCount / 2);
        var isBlackCardCountValid = cards.Select(x => x.Color)
            .Count(x => x == CardColor.Black).Equals(CardCount / 2);
        if (!isRedCardCountValid || !isBlackCardCountValid)
        {
            throw new ArgumentException("red cards and black cards must be equals 18");
        }
    }
}