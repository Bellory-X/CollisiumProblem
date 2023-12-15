using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Workers;
using GodsApi.Model;
using GodsApi.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GodsApi.Services;

public class GodsMassTransitHostedService(
    ILogger<GodsMassTransitHostedService> logger,
    IHostApplicationLifetime applicationLifetime,
    IExperimentRepository repository,
    IDeckShuffler shuffler,
    MassTransitWorker worker
    ) : IHostedService
{
    private readonly Card[] _cards = Deck.GetCards();

    public Task StartAsync(CancellationToken cancellationToken)
    {
        applicationLifetime.ApplicationStarted.Register(() =>
        {
            try { Run(); }
            catch (Exception e) { logger.LogError(e, "Unhandled exception!"); }
            finally { applicationLifetime.StopApplication(); }
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    
    private Task Run()
    {
        while (true)
        {
            var id = Convert.ToInt32(Console.ReadLine());
            if (id < 1) return Task.CompletedTask;

            var experiment = repository.GetExperimentById(id);
            if (experiment is null) RunNewExperiment(id);
            else RunOldExperiment(experiment);
            logger.LogInformation("Send orders with id = {}", id);
        }
    }

    private Task RunNewExperiment(int id)
    {
        var deck = shuffler.Shuffle(_cards);
        repository.AddExperiment(new Experiment(id, deck.Cards, null));
        return worker.Work(id, deck);
    }

    private Task RunOldExperiment(Experiment experiment)
    {
        return worker.Work(experiment.Id, new Deck { Cards = experiment.Cards });
    }
}