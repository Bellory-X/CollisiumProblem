using System.Net.Http.Json;
using System.Text;
using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Experiments;
using ColiseumLibrary.Model.Orders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ColiseumLibrary.Workers;

public class ExperimentWebService(
    ILogger<ExperimentWebService> logger,
    IExperimentRepository repository,
    IDeckShuffler shuffler
    ) : IExperimentService
{
    private const string PlayerUrl = "https://localhost:7212/player";
    private const string OpponentUrl = "https://localhost:7277/opponent";
    private readonly HttpClient _client = new();
    private readonly Card[] _cards = Deck.GetCards();

    public void Run()
    {
        var id = Convert.ToInt32(Console.ReadLine());
        if (id < 1) return;
        var experiment = repository.GetExperimentById(id);
        if (experiment is null) RunNewExperiment(id);
        else RunOldExperiment(experiment);
    }
    
    private void RunNewExperiment(int id)
    {
        var deck = shuffler.Shuffle(_cards);
        var output = Work(id, deck);
        repository.AddExperiment(new Experiment(id, deck.Cards, output));
        logger.LogInformation("Experiment: id = {}, output = {}", id, output);
    }

    private void RunOldExperiment(Experiment experiment)
    {
        var output = Work(experiment.Id, new Deck { Cards = experiment.Cards });
        logger.LogInformation("Experiment: id = {}, old output = {}, new output = {}", 
            experiment.Id, experiment.Output, output);
        if (experiment.Output != output) repository.AddExperiment(experiment with { Output = output });
    }

    private bool Work(int id, Deck deck)
    {
        var playerChoice = GetCardNumber(PlayerUrl, new Order {Id = id, Cards = deck.FirstHalf}).Result;
        var opponentChoice = GetCardNumber(OpponentUrl, new Order {Id = id, Cards = deck.SecondHalf}).Result;
        if (playerChoice is null || opponentChoice is null) throw new NullReferenceException();
        
        return deck.Cards[opponentChoice.CardNumber] == deck.Cards[playerChoice.CardNumber];
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