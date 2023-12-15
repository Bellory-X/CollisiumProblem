using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Experiments;
using ColiseumLibrary.Model.Orders;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ColiseumLibrary.Workers;

public class ExperimentMassTransitService(
    ILogger<ExperimentMassTransitService> logger,
    IExperimentRepository repository,
    IDeckShuffler shuffler,
    ISendEndpointProvider sendEndpointProvider
    ) : IExperimentService
{
    private const string PlayerUrl  = "queue:player"; 
    private const string OpponentUrl  = "queue:opponent";
    private readonly Card[] _cards = Deck.GetCards();
    
    public void Run()
    {
        var id = Convert.ToInt32(Console.ReadLine());
        if (id < 1) return;
        logger.LogInformation("Sending orders with id = {}", id);
        var experiment = repository.GetExperimentById(id);
        if (experiment is null) RunNewExperiment(id);
        else RunOldExperiment(experiment);
    }

    private Task RunNewExperiment(int id)
    {
        var deck = shuffler.Shuffle(_cards);
        repository.AddExperiment(new Experiment(id, deck.Cards, null));
        return Work(id, deck);
    }

    private Task RunOldExperiment(Experiment experiment) => 
        Work(experiment.Id, new Deck { Cards = experiment.Cards });

    private async Task Work(int id, Deck deck)
    {
        await Send(PlayerUrl, new Order { Id = id, Cards = deck.FirstHalf });
        await Send(OpponentUrl, new Order { Id = id, Cards = deck.SecondHalf });
    }

    private async Task Send(string url, Order order)
    {
        var playerEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(url)); 
        await playerEndpoint.Send(order);
    }
}