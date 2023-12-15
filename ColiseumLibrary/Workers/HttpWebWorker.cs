using System.Net.Http.Json;
using System.Text;
using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.Orders;
using Newtonsoft.Json;

namespace ColiseumLibrary.Workers;

public class HttpWebWorker
{
    private readonly HttpClient _client = new();
    public string PlayerUrl { get; set; } = "https://localhost:7212/player";
    public string OpponentUrl { get; set; } = "https://localhost:7277/opponent";

    public bool Work(int id, Deck deck)
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