using System.Collections.Immutable;
using ColiseumLibrary.Contracts.Cards;
using GodsApi.Models;
using GodsApi.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GodsApi.Services;

public class GodsHostedService(
        ILogger<GodsHostedService> logger,
        IHostApplicationLifetime appLifetime,
        IExperimentRepository repository,
        IWorker worker
    )
    : IHostedService
{
    private const int ExperimentCount = 10;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        appLifetime.ApplicationStarted.Register(() =>
        {
            try
            {
                Run();
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

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private void Run()
    {
        var firstPass = RunExperiments();
        repository.AddExperiments(firstPass.ToImmutableList());
        var compareResult = RunAndCompareExperiments(repository.GetLatestExperiments(ExperimentCount));
        double successCount = firstPass.Sum(x => x.Output ? 1 : 0);
        logger.LogInformation("Experiment result: {}%", successCount / ExperimentCount * 100);
        logger.LogInformation("Comparison result: {}", compareResult);
    }

    private List<Experiment> RunExperiments()
    {
        var experiments = new List<Experiment>();
        for (var id = 1; id <= ExperimentCount; id++)
        {
            var deck = new Deck();
            experiments.Add(
                new Experiment(
                    id, 
                    deck.FirstHalf, 
                    deck.SecondHalf, 
                    worker.RunExperiment(deck.FirstHalf, deck.SecondHalf).Result
                    )
                );
        }
        return experiments;
    }

    private bool RunAndCompareExperiments(IEnumerable<Experiment> firstPass)
    {
        foreach (var experiment in firstPass)
        {
            var output = worker.RunExperiment(experiment.PlayerCards, experiment.OpponentCards).Result;
            if (output != experiment.Output)
            {
                return false;
            }
        }
        return true;
    }

}