using System.Collections.Immutable;
using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.DeckShufflers;
using ColiseumLibrary.Contracts.ExperimentWorkers;
using GodsApi.Models;
using GodsApi.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GodsApi.Services;

public class HostedService(
        ILogger<HostedService> logger,
        IHostApplicationLifetime appLifetime,
        IExperimentRepository repository,
        IDeckShuffler shuffler,
        IExperimentWorker worker
    )
    : IHostedService
{
    private const int ExperimentCount = 10;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        appLifetime.ApplicationStarted.Register(() =>
        {
            try { Run(); }
            catch (Exception e) { logger.LogError(e, "Unhandled exception!"); }
            finally { appLifetime.StopApplication(); }
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private void Run()
    {
        var firstPass = RunExperiments();
        
        repository.AddExperiments(firstPass.ToImmutableList());
        var secondPass = repository.GetLatestExperiments(firstPass.Count);
        
        var compareResult = RunAndCompareExperiments(secondPass);
        double successCount = firstPass.Sum(x => x.Output ? 1 : 0);
        
        logger.LogInformation("Experiment result: {}%", successCount / firstPass.Count * 100);
        logger.LogInformation("Comparison result: {}", compareResult);
        // logger.LogInformation("Experiments: {}", secondPass);
    }

    private List<Experiment> RunExperiments()
    {
        var cards = GetCards();
        var experiments = new List<Experiment>();
        for (var id = 1; id <= ExperimentCount; id++)
        {
            var deck = shuffler.Shuffle(cards);
            experiments.Add(new Experiment(id, deck.Cards, worker.RunExperiment(deck).Result));
        }
        return experiments;
    }

    private bool RunAndCompareExperiments(List<Experiment> firstPass)
    {
        foreach (var experiment in firstPass)
        {
            var deck = new Deck(experiment.Cards);
            var output = worker.RunExperiment(deck).Result;
            if (output != experiment.Output)
            {
                return false;
            }
        }
        return true;
    }

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

}