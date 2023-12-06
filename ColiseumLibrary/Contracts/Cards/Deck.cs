namespace ColiseumLibrary.Contracts.Cards;

/// <summary>
/// Колода карт
/// </summary>
public class Deck
{
    private const int CardCount = 18;
    public Card[] FirstHalf { get; }
    public Card[] SecondHalf { get; }

    public Deck()
    {
        var cards = new Card[CardCount * 2];
        var random = new Random();
        for (var i = 0; i < CardCount; i++)
        {
            cards[i] = new Card(CardColor.Red);
            cards[cards.Length - 1 - i] = new Card(CardColor.Black);
        }
        for (var i = 0; i < CardCount; i++)
        {
            var j = random.Next(CardCount * 2);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
        FirstHalf = cards.Take(CardCount).ToArray();
        SecondHalf = cards.TakeLast(CardCount).ToArray();
    }
}