using System.Collections;
using System.Text;
using ColiseumLibrary.Contracts.Cards;
using Newtonsoft.Json;

namespace ColiseumLibrary.Contracts.ExperimentWorkers;

public class HttpExperimentWorker : IExperimentWorker
{
    private const string FirstPlayerUrl = "https://localhost:7212/api/first";
    private const string SecondPlayerUrl = "https://localhost:7277/api/second";
    private readonly HttpClient _client = new();

    public async Task<bool> RunExperiment(Deck deck)
    {
        var firstPlayerChoice = await GetPlayerChoice(FirstPlayerUrl, deck.FirstHalf);
        var secondPlayerChoice = await GetPlayerChoice(SecondPlayerUrl, deck.SecondHalf);
        
        return deck.Cards[secondPlayerChoice] == deck.Cards[firstPlayerChoice];
    }

    private async Task<int> GetPlayerChoice(string url, IEnumerable cards)
    {
        var jsonRequest = JsonConvert.SerializeObject(cards);
        var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(url, requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<int>(responseContent);
    }
    
}