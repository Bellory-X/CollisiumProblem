using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;

namespace ColiseumLibrary.Strategies;

public class LastCardStrategy : ICardPickStrategy
{
    public int Pick(Card[] cards) => cards.Length - 1;
}