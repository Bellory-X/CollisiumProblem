using ColiseumLibrary.Contracts.Cards;

namespace ColiseumLibrary.Contracts.Strategies;

/// <summary>
/// Стратегия выбора рандомной карты из колоды
/// </summary>
public class RandomCardStrategy : ICardPickStrategy
{
    private readonly Random _random = new();

    public int Pick(Card[] cards) => _random.Next(0, cards.Length);
}