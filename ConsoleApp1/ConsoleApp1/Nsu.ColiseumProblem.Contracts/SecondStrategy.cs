using ConsoleApp1.Nsu.ColiseumProblem.Contracts.Cards;

namespace ConsoleApp1.Nsu.ColiseumProblem.Contracts;

public class SecondStrategy : ICardPickStrategy
{
    public int Pick(Card[] cards)
    {
        var redCardCount = cards.Aggregate(0, (curr, card) => curr + (card.Color == CardColor.Red ? 1 : 0));
        var random = new Random();
        return redCardCount >= 9 ? random.Next(0, cards.Length) : 0;
    }
}