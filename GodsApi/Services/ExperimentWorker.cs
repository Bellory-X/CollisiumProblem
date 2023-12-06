using System.Collections;
using System.Text;
using ColiseumLibrary.Contracts.Cards;
using Newtonsoft.Json;

namespace GodsApi.Services;

public class ExperimentWorker : IWorker
{
    private const string FirstPlayerUrl = "https://localhost:7212/api/first";
    private const string SecondPlayerUrl = "https://localhost:7277/api/second";
    private readonly HttpClient _client = new();

    public async Task<bool> RunExperiment(Card[] playerCards, Card[] opponentCards)
    {
        var firstPlayerChoice = await GetPlayerChoice(FirstPlayerUrl, playerCards);
        var secondPlayerChoice = await GetPlayerChoice(SecondPlayerUrl, opponentCards);
        
        return playerCards[secondPlayerChoice] == opponentCards[firstPlayerChoice];
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