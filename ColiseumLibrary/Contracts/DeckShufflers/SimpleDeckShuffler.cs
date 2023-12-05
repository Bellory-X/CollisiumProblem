using ColiseumLibrary.Contracts.Cards;

namespace ColiseumLibrary.Contracts.DeckShufflers;

/// <summary>
/// Простой тасовщик колод
/// </summary>
public class SimpleDeckShuffler : IDeckShuffler
{
    private const int CardCount = 18;
    
    public void ShuffleDeck(out Card[] firstDeck, out Card[] secondDeck)
    {
        firstDeck = new Card[CardCount];
        secondDeck = new Card[CardCount];
        var random = new Random();
        var redCardCount = 0;

        for (var index = 0; index < CardCount; index++)
        {
            if (random.Next(0, 2) == 1 && redCardCount < CardCount)
            {
                firstDeck[index] = new Card(CardColor.Red);
                redCardCount++;
                continue;
            }
            firstDeck[index] = new Card(CardColor.Black);
            if (random.Next(0, 2) == 1 && redCardCount < CardCount)
            {
                secondDeck[index] = new Card(CardColor.Red);
                redCardCount++;
                continue;
            }
            secondDeck[index] = new Card(CardColor.Black);
        }
    }
}