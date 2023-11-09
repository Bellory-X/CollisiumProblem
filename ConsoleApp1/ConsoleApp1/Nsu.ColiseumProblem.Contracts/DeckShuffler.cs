using ConsoleApp1.Nsu.ColiseumProblem.Contracts.Cards;

namespace ConsoleApp1.Nsu.ColiseumProblem.Contracts;

public struct Deck
{
    public Card[] FirstDeck { get; init; }
    public Card[] SecondDeck { get; init; }

    public Deck(int deckSize)
    {
        FirstDeck = new Card[deckSize];
        SecondDeck = new Card[deckSize];
    }
}
public class DeckShuffler
{
    private const int DeckSize = 18;
    private readonly Random _random;
    public Deck Deck { get; init; }
    public DeckShuffler()
    {
        Deck = new Deck(DeckSize);
        _random = new Random();
        Stir();
    }

    public void Stir()
    {
        var countRedCards = FillFirstDeck();
        FillSecondDeck(countRedCards);
    }

    private int FillFirstDeck()
    {
        var countRedCards = DeckSize;
        for(var index = 0; index < DeckSize; index++)
        {
            if (_random.Next(0, 2) == 1)
            {
                Deck.FirstDeck[index] = new Card(CardColor.Red);
                countRedCards--;
            }
            else
            {
                Deck.FirstDeck[index] = new Card(CardColor.Black);
            }
        }
        return countRedCards;
    }

    private void FillSecondDeck(int countRedCards)
    {
        var index = 0;
        var countBlackCards = DeckSize - countRedCards;
        for (; countRedCards > 0 && countBlackCards > 0; index++)
        {
            if (_random.Next(0, 2) == 1)
            {
                Deck.SecondDeck[index] = new Card(CardColor.Red);
                countRedCards--;
            }
            else
            {
                Deck.SecondDeck[index] = new Card(CardColor.Black);
                countBlackCards--;
            }
        }
        if (countRedCards == 0)
        {
            for (; index < DeckSize; index++)
            {
                Deck.SecondDeck[index] = new Card(CardColor.Black);
            }
            return;
        }
        for (; index < DeckSize; index++)
        {
            Deck.SecondDeck[index] = new Card(CardColor.Red);
        }
    }
}