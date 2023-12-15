using System.Collections.Immutable;
using AutoMapper;
using ColiseumLibrary.DeckShufflers;
using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Experiments;
using ColiseumLibrary.Strategies;
using GodsApi.Data;
using ColiseumLibrary.Workers;
using GodsApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace TestColiseumProblem;

public class DbTests
{
    private static ExperimentRepository GetRepository()
    {
        var options = new DbContextOptionsBuilder<ExperimentDbContext>()
            .UseSqlite("FileName=test_db.db").Options;
        // var mapper = new MapperConfiguration(cfg => 
        //         cfg.AddProfile<MappingProfiles>()).CreateMapper();
        var context = new ExperimentDbContext(options);
        
        return new ExperimentRepository(context);
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
        var shuffler = new NotDeckShuffler();
        var firstCard = new FirstCardStrategy();
        var lastCard = new LastCardStrategy();
        var worker = new SimpleExperimentService(shuffler, firstCard, lastCard);
        var firstPass = new List<Experiment>();
        for (var id = 1; id <= 100; id++)
        {
            worker.Run();
            firstPass.Add(new Experiment(id, worker.Cards.ToImmutableArray(), worker.Output));
        }
        worker.Shuffler = new NotDeckShuffler();
        
        repository.AddExperiments(firstPass.ToImmutableList());
        var secondPass = repository.GetExperiments(100);
        
        
        Assert.Multiple(() =>
        {
            foreach (var experiment in secondPass)
            {
                worker.Cards = experiment.Cards.ToArray();
                worker.Run();
                Assert.That(worker.Output == experiment.Output, Is.EqualTo(true));
            }
        });
    }
}