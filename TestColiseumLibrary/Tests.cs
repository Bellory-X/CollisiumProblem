using System.Collections.Immutable;
using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.DeckShufflers;
using ColiseumLibrary.Contracts.Strategies;

namespace TestColiseumLibrary;

public class Tests
{
    private const int CardCount = 18;

    [Test]
    public void TestDeck_With_CorrectCardArray()
    {
        var cards = new Card[Deck.CardCount];
        for (var i = 0; i < Deck.CardCount / 2; i++)
        {
            cards[i] = new Card(CardColor.Red);
            cards[cards.Length - 1 - i] = new Card(CardColor.Black);
        }
        var shuffler = new RandomDeckShuffler();
        var deck = shuffler.Shuffle(cards);
        
        var redCardCount = deck.Cards.Select(x => x.Color).Count(x => x == CardColor.Red);
        var blackCardCount = deck.Cards.Select(x => x.Color).Count(x => x == CardColor.Black);
        
        Assert.That(redCardCount, Is.EqualTo(18));
        Assert.That(blackCardCount, Is.EqualTo(18));
    }
    
    [Test]
    public void TestDeck_With_OnlyBlackColorCardArray()
    {
        var cards = new Card[CardCount * 2];
        for (var i = 0; i < cards.Length; i++)
        {
            cards[i] = new Card(CardColor.Black);
        }
        var shuffler = new RandomDeckShuffler();
        
        Assert.That(
            Assert.Throws<ArgumentException>(() => shuffler.Shuffle(cards))?.Message, 
            Is.EqualTo("red cards and black cards must be equals 18"));
    }
    
    [Test]
    public void TestFirstCardStrategy_With_EmptyCardArray()
    {
        var cards = new Card[CardCount];
        var strategy = new FirstCardStrategy();
        
        var cardNumber = strategy.Pick(cards);
        
        Assert.That(cardNumber, Is.EqualTo(0));
    }

    [Test]
    public void TestExperiment_With_FirstCardStrategies_And_RandomDeckShuffler()
    {
        Assert.Pass();
    }
    
}