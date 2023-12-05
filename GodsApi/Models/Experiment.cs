using ColiseumLibrary.Contracts.Cards;

namespace GodsApi.Models;

public record Experiment(int Id, Card[] PlayerDeck, Card[] OpponentDeck, bool Output) : IComparable
{
    public int CompareTo(object? o)
    {
        if(o is Experiment experiment)
        {
            return Id.CompareTo(experiment.Id);
        }
        throw new ArgumentException("Некорректное значение параметра");
    }
}