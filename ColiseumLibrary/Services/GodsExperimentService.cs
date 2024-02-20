using System.Net.Http.Json;
using System.Text;
using ColiseumLibrary.DeckShufflers;
using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Experiments;
using ColiseumLibrary.Model.Orders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ColiseumLibrary.Services;

public class GodsExperimentService(
    ILogger<GodsExperimentService> logger,
    IExperimentRepository repository,
    IDeckShuffler shuffler
    ) : IExperimentService
{
    private const string PlayerUrl = "https://localhost:7212/player";
    private const string OpponentUrl = "https://localhost:7277/opponent";
    private readonly HttpClient _client = new();
    private readonly Card[] _cards = Deck.GetCards();

    public Task Run(int id)
    {
        var experiment = repository.GetExperimentById(id);
        return experiment is null ? RunNewExperiment(id) : RunOldExperiment(experiment);
    }
    
    private Task RunNewExperiment(int id)
    {
        var deck = shuffler.Shuffle(_cards);
        var output = Work(id, deck);
        repository.AddExperiment(new Experiment(id, deck.Cards, output.Result));
        logger.LogInformation("Experiment: id = {}, output = {}", id, output);
        
        return Task.CompletedTask;
    }

    private Task RunOldExperiment(Experiment experiment)
    {
        var output = Work(experiment.Id, new Deck(experiment.Cards));
        if (experiment.Output != output.Result)
        {
            repository.AddExperiment(experiment with { Output = output.Result });
        }
        logger.LogInformation("Experiment: id = {}, old output = {}, new output = {}", 
            experiment.Id, experiment.Output, output);
        
        return Task.CompletedTask;
    }

    private async Task<bool> Work(int id, Deck deck)
    {
        switch (shuffler)
        {
            case NotDeckShuffler: 
                break;
            case RandomDeckShuffler:
                break;
        }
        var playerChoice = await GetCardNumber(PlayerUrl, new Order {Id = id, Cards = deck.FirstHalf});
        var opponentChoice = await GetCardNumber(OpponentUrl, new Order {Id = id, Cards = deck.SecondHalf});
        ArgumentNullException.ThrowIfNull(playerChoice);
        ArgumentNullException.ThrowIfNull(opponentChoice);
        
        return deck.Cards[opponentChoice.Ordinal] == deck.Cards[playerChoice.Ordinal];
    }

    private async Task<OrderCreated?> GetCardNumber(string url, Order order)
    {
        var jsonRequest = JsonConvert.SerializeObject(order);
        var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(url, requestContent);
        var responseContent = await response.Content.ReadFromJsonAsync<OrderCreated>();
        
        return responseContent;
    }
}