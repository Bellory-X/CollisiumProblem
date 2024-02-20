using System.Net.Http.Json;
using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Experiments;
using ColiseumLibrary.Model.Orders;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ColiseumLibrary.Services;

public class GodsHardExperimentService(
    ILogger<GodsHardExperimentService> logger,
    // IExperimentRepository repository,
    IDeckShuffler shuffler,
    ISendEndpointProvider sendEndpointProvider
    ) : IExperimentService
{
    private const string PlayerEndpointUrl  = "exchange:player";
    private const string OpponentEndpointUrl  = "queue:opponent";
    // private const string PlayerClientUrl = "https://localhost:7212/player";
    // private const string OpponentClientUrl = "https://localhost:7277/opponent";
    // private readonly HttpClient _client = new();
    // private readonly Dictionary<int, OrderCreated> _choiceMap = new();
    // // private readonly Mutex _mutex = new();
    private readonly Card[] _cards = Deck.GetCards();
    
    public async Task Run(int id)
    {
        // var experiment = repository.GetExperimentById(id);
        // if (experiment is not null)
        // {
        //     await Send(experiment.Id, new Deck { Cards = experiment.Cards });
        //     return;
        // }
        var deck = shuffler.Shuffle(_cards);
        // repository.AddExperiment(new Experiment(id, deck.Cards, null));
        await Send(id, deck);
    }

    private async Task Send(int id, Deck deck)
    {
        var playerEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(PlayerEndpointUrl)); 
        await playerEndpoint.Send(new Order{Id = id, Cards = deck.FirstHalf});
        // var opponentEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(OpponentEndpointUrl)); 
        // await opponentEndpoint.Send(new Order(id, deck.SecondHalf));
        logger.LogInformation("Sending orders with id = {}", id);
    }
    
    // public Task Run(OrderCreated order) => _choiceMap.ContainsKey(order.Id) ? Work(order) : AddChoice(order);
    //
    // private Task AddChoice(OrderCreated order)
    // {
    //     _choiceMap.Add(order.Id, order);
    //     logger.LogInformation("Add choice: id: {}, value: {}", order.Id, order.Ordinal);
    //     return Task.CompletedTask;
    // }
    //
    // private async Task Work(OrderCreated order)
    // {
    //     var playerChoice = await GetCard(PlayerClientUrl, order.IsPlayer ? order : _choiceMap[order.Id]);
    //     var opponentChoice = await GetCard(OpponentClientUrl, order.IsPlayer ? _choiceMap[order.Id] : order);
    //     if (playerChoice is null || opponentChoice is null)
    //     {
    //         throw new NullReferenceException();
    //     }
    //     var output = playerChoice.Color == opponentChoice.Color;
    //     var experiment = repository.GetExperimentById(order.Id);
    //     ArgumentNullException.ThrowIfNull(experiment);
    //     if (experiment.Output is not null)
    //     {
    //         logger.LogInformation("Experiment: id: {}, new output: {}, old output: {}",
    //             order.Id, output, experiment.Output);
    //     }
    //     else
    //     {
    //         logger.LogInformation("Experiment: id: {}, output: {}", order.Id, output);
    //     }
    //     repository.AddExperiment(experiment with { Output = output });
    //     _choiceMap.Remove(order.Id);
    // }
    //
    // private async Task<Card?> GetCard(string url, OrderCreated order)
    // {
    //     var response = await _client.GetAsync(url + '/' + order.Id + '/' + order.Ordinal);
    //     var responseContent = await response.Content.ReadFromJsonAsync<Card>();
    //
    //     return responseContent;
    // }
}