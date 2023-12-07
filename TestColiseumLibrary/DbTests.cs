using System.Collections.Immutable;
using System.Data;
using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.DeckShufflers;
using ColiseumLibrary.Contracts.ExperimentWorkers;
using ColiseumLibrary.Contracts.Strategies;
using GodsApi.Models;
using TestColiseumLibrary.Data;

namespace TestColiseumLibrary;

public class DbTests
{
    private static Card[] GetCards()
    {
        var cards = new Card[Deck.CardCount];
        for (var i = 0; i < Deck.CardCount / 2; i++)
        {
            cards[i] = new Card(CardColor.Black);
            cards[Deck.CardCount - 1 - i] = new Card(CardColor.Red);
        }
        return cards;
    }
    
    private static string Convert(Card[] domainModel) =>
        String.Join('\n', domainModel.Select(x => x.ToString()));
    
    private static ImmutableArray<Card> Convert(string dbModel)
    {
        var domainModel = dbModel.Split('\n');
        if (domainModel.Length != 36) throw new DataException("colors not equals 36");

        return Array.ConvertAll(domainModel, s => {
            return s switch 
            { 
                "♠️" => new Card(CardColor.Black), 
                "♦️" => new Card(CardColor.Red), 
                _ => throw new DataException("color not exist"), 
            };
        }).ToImmutableArray();
    }
    
    [Test]
    public void SaveExperimentsInDataBase()
    {
        var dbContext = new TestDbContext();
        var cards = GetCards();
        var firstPass = new ExperimentDbModel();
        firstPass.Id = 1;
        firstPass.CardColors = Convert(cards);
        firstPass.Output = true;

        dbContext.ExperimentDbModels.Add(firstPass);
        dbContext.SaveChanges();
        var secondPass = dbContext.ExperimentDbModels.ToList();
        
        Assert.That(secondPass.Count, Is.EqualTo(1));
        Assert.Contains(firstPass, secondPass);
    }
    
    [Test]
    public void EqualsExperimentsResults()
    {
        var dbContext = new TestDbContext();
        var cards = GetCards();
        var shuffler = new RandomDeckShuffler();
        var playerStrategy = new FirstCardStrategy();
        var opponentStrategy = new LastCardStrategy();
        var worker = new SimpleExperimentWorker(playerStrategy, opponentStrategy);
        var firstPass = new List<ExperimentDbModel>();
        for (var id = 1; id <= 100; id++)
        {
            var deck = shuffler.Shuffle(cards);
            var experiment = new ExperimentDbModel();
            experiment.Id = id;
            experiment.CardColors = Convert(deck.Cards.ToArray());
            experiment.Output = worker.RunExperiment(deck).Result;
            firstPass.Add(experiment);
        }
        
        dbContext.ExperimentDbModels.AddRange(firstPass);
        dbContext.SaveChanges();
        var secondPass = dbContext.ExperimentDbModels.ToList();
        var equalsExperiments = true;
        foreach (var experiment in secondPass)
        {
            var deck = new Deck(Convert(experiment.CardColors));
            var output = worker.RunExperiment(deck).Result;
            if (output != experiment.Output)
            {
                equalsExperiments = false;
                break;
            }
        }
        
        Assert.That(equalsExperiments, Is.EqualTo(true));
    }
}