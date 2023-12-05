using System.Text;
using AutoMapper;
using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.DeckShufflers;
using GodsApi.Data;
using GodsApi.Models;
using GodsApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GodsApi.Services;

public class GodsHostedService(
        ILogger<IHostedService> logger,
        IHostApplicationLifetime appLifetime,
        IDeckShuffler deckShuffler,
        IWorker worker
    )
    : IHostedService
{
    private const int ExperimentCount = 100;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        appLifetime.ApplicationStarted.Register(() =>
        {
            try
            {
                RunExperiments();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unhandled exception!");
            }
            finally
            {
                appLifetime.StopApplication();
            }
        });
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void RunExperiments()
    {
        var successCount = 0.0;
        var experiments = GetExperiments();

        logger.LogInformation("Result: {}%", successCount / ExperimentCount * 100);
    }

    private SortedSet<Experiment> GetExperiments()
    {
        var experiments = new SortedSet<Experiment>();
        for (var i = 0; i < ExperimentCount; i++)
        {
            deckShuffler.ShuffleDeck(out var firstDeck,out var secondDeck);
            var experiment = worker.RunExperiment(i, firstDeck, secondDeck).Result;
            experiments.Add(experiment);
        }

        return experiments;
    }

}