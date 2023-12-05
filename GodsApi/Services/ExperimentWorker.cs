using System.Text;
using ColiseumLibrary.Contracts.Cards;
using GodsApi.Models;
using Newtonsoft.Json;

namespace GodsApi.Services;

public class ExperimentWorker : IWorker
{
    private const string FirstPlayerUrl = "https://localhost:7212/api/first";
    private const string SecondPlayerUrl = "https://localhost:7277/api/second";
    private readonly HttpClient _client;

    public ExperimentWorker()
    {
        _client = new HttpClient();
    }
    public async Task<Experiment> RunExperiment(int ordinal, Card[] firstDeck, Card[] secondDeck)
    {
        var firstPlayerChoice = await GetPlayerChoice(FirstPlayerUrl, firstDeck);
        var secondPlayerChoice = await GetPlayerChoice(SecondPlayerUrl, secondDeck);

        return new Experiment(
            ordinal, 
            firstDeck, 
            secondDeck, 
            firstDeck[secondPlayerChoice] == secondDeck[firstPlayerChoice]
            );
    }

    private async Task<int> GetPlayerChoice(string url, Card[] deck)
    {
        var jsonRequest = JsonConvert.SerializeObject(deck);
        var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(url, requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<int>(responseContent);
    }
    
}