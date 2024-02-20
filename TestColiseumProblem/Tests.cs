using System.Collections.Immutable;
using ColiseumLibrary.DeckShufflers;
using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Services;
using ColiseumLibrary.Strategies;

namespace TestColiseumProblem;

public class Tests
{

    [Test]
    public void TestDeckWithCorrectCardArray_ShouldReturnDeck()
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
        
        
        Assert.Multiple(() =>
        {
            Assert.That(redCardCount, Is.EqualTo(18));
            Assert.That(blackCardCount, Is.EqualTo(18));
        });
    }

    [Test]
    public void TestDeckWithIncorrectCardArraySize_ShouldThrowException()
    {
        var blackCards = new Card[Deck.CardCount - 1];
        for (var i = 0; i < blackCards.Length; i++)
        {
            blackCards[i] = new Card(CardColor.Black);
        }
        
        
        Assert.That(
            Assert.Throws<ArgumentException>(() => new Deck(blackCards.ToImmutableArray()))?.Message, 
            Is.EqualTo("firstHalf and secondHalf must be equals 32"));
    }
    
    [Test]
    public void TestDeckWithIncorrectColorCards_ShouldThrowException()
    {
        var blackCards = new Card[Deck.CardCount];
        for (var i = 0; i < blackCards.Length; i++)
        {
            blackCards[i] = new Card(CardColor.Black);
        }
        
        
        Assert.That(
            Assert.Throws<ArgumentException>(() => new Deck(blackCards.ToImmutableArray()))?.Message, 
            Is.EqualTo("red cards and black cards must be equals 18"));
    }
    
    [Test]
    public void TestFirstCardStrategy_ShouldReturnZeroNumber()
    {
        var cards = new Card[Deck.CardCount];
        var strategy = new FirstCardStrategy();
        
        
        var cardNumber = strategy.Pick(cards);
        
        
        Assert.That(cardNumber, Is.EqualTo(0));
    }

    [Test]
    public void TestExperimentWithFirstCardStrategiesAndNotDeck_ShufflerShouldReturnFalse()
    {
        var cards = new Card[Deck.CardCount];
        for (var i = 0; i < Deck.CardCount / 2; i++)
        {
            cards[i] = new Card(CardColor.Red);
            cards[cards.Length - 1 - i] = new Card(CardColor.Black);
        }
        var shuffler = new NotDeckShuffler();
        var firstCard = new FirstCardStrategy();
        var lastCard = new LastCardStrategy();
        var worker = new SimpleExperimentService(shuffler, firstCard, lastCard);
        
        
        worker.Run(1);
        
        
        Assert.That(worker.Output, Is.EqualTo(false));
    }
    
}