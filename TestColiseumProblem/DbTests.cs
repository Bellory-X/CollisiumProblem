using System.Collections.Immutable;
using AutoMapper;
using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.Strategies;
using ColiseumLibrary.DeckShufflers;
using GodsApi.Data;
using ColiseumLibrary.Workers;
using GodsApi.Model;
using GodsApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace TestColiseumProblem;

public class DbTests
{
    private static ExperimentRepository GetRepository()
    {
        var options = new DbContextOptionsBuilder<ExperimentDbContext>()
            .UseSqlite("FileName=test_db.db").Options;
        var mapper = new MapperConfiguration(cfg => 
                cfg.AddProfile<MappingProfiles>()).CreateMapper();
        var context = new ExperimentDbContext(options);
        
        return new ExperimentRepository(context, mapper);
    }
    
    [Test]
    public void SaveExperimentsInDataBase_ShouldBeSave()
    {
        var repository = GetRepository();
        var cards = Deck.GetCards();
        var shuffler = new RandomDeckShuffler();
        var firstPass =  new Experiment(1, shuffler.Shuffle(cards).Cards, true);

        
        repository.AddExperiment(firstPass);
        var secondPass = repository.GetExperimentById(firstPass.Id);
        
        
        Assert.That(secondPass, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(secondPass.Id, Is.EqualTo(firstPass.Id));
            Assert.That(secondPass.Cards, Is.EqualTo(firstPass.Cards));
            Assert.That(secondPass.Output, Is.EqualTo(firstPass.Output));
        });
    }

    [Test]
    public void EqualsExperimentsResults_ShouldBeEquals()
    {
        var repository = GetRepository();
        var cards = Deck.GetCards();
        var shuffler = new RandomDeckShuffler();
        var worker = new SimpleWorker(new FirstCardStrategy(), new LastCardStrategy());
        var firstPass = new List<Experiment>();
        for (var id = 1; id <= 100; id++)
        {
            var deck = shuffler.Shuffle(cards);
            var output = worker.Work(deck);
            firstPass.Add(new Experiment(id, deck.Cards, output));
        }

        
        repository.AddExperiments(firstPass.ToImmutableList());
        var secondPass = repository.GetExperiments(100);
        
        
        Assert.Multiple(() =>
        {
            foreach (var experiment in secondPass)
            {
                var deck = new Deck { Cards = experiment.Cards };
                var output = worker.Work(deck);
                Assert.That(output == experiment.Output, Is.EqualTo(true));
            }
        });
    }
}