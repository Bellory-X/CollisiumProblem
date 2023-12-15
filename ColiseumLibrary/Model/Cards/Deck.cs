using System.Collections.Immutable;

namespace ColiseumLibrary.Model.Cards;

/// <summary>
/// Колода карт
/// </summary>
public record Deck
{
    public const int CardCount = 36;
    private readonly ImmutableArray<Card> _cards;

    public ImmutableArray<Card> Cards 
    { 
        get => _cards;
        init
        {
            ValidateCards(value);
            _cards = value;
        }
    }
    public ImmutableArray<Card> FirstHalf { get => Cards.Take(CardCount / 2).ToImmutableArray(); }
    public ImmutableArray<Card> SecondHalf{ get => Cards.TakeLast(CardCount / 2).ToImmutableArray(); }
    
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
        if (cards == null) throw new ArgumentNullException(nameof(cards));
        if (cards.Length != CardCount)
        {
            throw new ArgumentException("firstHalf and secondHalf must be equals 32");
        }
        var redCardCount = cards.Select(x => x.Color).Count(x => x == CardColor.Red);
        var blackCardCount = cards.Select(x => x.Color).Count(x => x == CardColor.Black);
        if (redCardCount != CardCount / 2 || blackCardCount != CardCount / 2)
        {
            throw new ArgumentException("red cards and black cards must be equals 18");
        }
    }
}