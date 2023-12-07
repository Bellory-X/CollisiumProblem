using System.Collections.Immutable;

namespace ColiseumLibrary.Contracts.Cards;

/// <summary>
/// Колода карт
/// </summary>
public record Deck
{
    public const int CardCount = 36;
    public ImmutableArray<Card> Cards { get; }
    public ImmutableArray<Card> FirstHalf { get => Cards.Take(CardCount / 2).ToImmutableArray(); }
    public ImmutableArray<Card> SecondHalf{ get => Cards.Take(CardCount / 2).ToImmutableArray(); }

    public Deck(ImmutableArray<Card> cards)
    {
        ValidateCards(cards);
        Cards = cards;
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