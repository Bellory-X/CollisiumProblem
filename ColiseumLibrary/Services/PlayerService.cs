using System.Collections.Immutable;
using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Orders;
using ColiseumLibrary.Strategies;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ColiseumLibrary.Services;

public class PlayerService(
    ICardPickStrategy strategy,
    ILogger<PlayerService> logger
    )
{
    // private readonly Dictionary<int, ImmutableArray<Card>> _cardsMap = new();
    
    public OrderCreated CreateOrder(Order order, bool isPlayer)
    {
        var cardNumber = strategy.Pick(order.Cards.ToArray());
        // if (!_cardsMap.ContainsKey(order.Id))
        // {
        //     _cardsMap.Add(order.Id, order.Cards);
        // }
        logger.LogInformation("Experiment id: {}, card number: {}", order.Id, cardNumber);
        return new OrderCreated{Id = order.Id, Ordinal = cardNumber, IsPlayer = isPlayer};
    }

    // public Card GetCard(int id, int ordinal)
    // {
    //     logger.LogInformation("Get by id: {}, card number: {}", id, ordinal);
    //     var card = _cardsMap[id][ordinal];
    //     _cardsMap.Remove(id);
    //     return card;
    // }
}