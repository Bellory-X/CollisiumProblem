using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;

namespace ColiseumLibrary.Strategies;

/// <summary>
/// Стратегия выбора рандомной карты из колоды
/// </summary>
public class RandomCardStrategy : ICardPickStrategy
{
    private readonly Random _random = new();

    public int Pick(Card[] cards) => _random.Next(0, cards.Length);
}