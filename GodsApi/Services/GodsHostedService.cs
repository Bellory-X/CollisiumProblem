using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Workers;
using GodsApi.Model;
using GodsApi.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GodsApi.Services;

public class GodsHostedService(
    ILogger<GodsHostedService> logger,
    IHostApplicationLifetime appLifetime,
    IExperimentRepository repository,
    IDeckShuffler shuffler,
    HttpWebWorker worker
    ) : IHostedService
{
    private readonly Card[] _cards = Deck.GetCards();

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
        while (true)
        {
            var id = Convert.ToInt32(Console.ReadLine());
            if (id < 1) return;
            
            var experiment = repository.GetExperimentById(id);
            if (experiment is null) RunNewExperiment(id);
            else RunOldExperiment(experiment);
        }
    }

    private void RunNewExperiment(int id)
    {
        var deck = shuffler.Shuffle(_cards);
        var output = worker.Work(id, deck);
        repository.AddExperiment(new Experiment(id, deck.Cards, output));
        logger.LogInformation("Experiment: id = {}, output = {}", id, output);
    }

    private void RunOldExperiment(Experiment experiment)
    {
        var output = worker.Work(experiment.Id, new Deck { Cards = experiment.Cards });
        logger.LogInformation("Experiment: id = {}, old output = {}, new output = {}", 
            experiment.Id, experiment.Output, output);
        if (experiment.Output != output) repository.AddExperiment(experiment with { Output = output });
    }
}