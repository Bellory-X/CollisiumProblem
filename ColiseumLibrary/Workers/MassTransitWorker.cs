using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.Orders;
using MassTransit;

namespace ColiseumLibrary.Workers;

public class MassTransitWorker(ISendEndpointProvider sendEndpointProvider)
{
    public string PlayerUrl { get; set; } = "queue:player";
    public string OpponentUrl { get; set; } = "queue:opponent";

    public async Task Work(int id, Deck deck)
    {
        await Send(PlayerUrl, new Order { Id = id, Cards = deck.FirstHalf });
        await Send(OpponentUrl, new Order { Id = id, Cards = deck.SecondHalf });
    }

    private async Task Send(string url, Order order)
    {
        var playerEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(url)); 
        await playerEndpoint.Send<Order>(order);
    }
}