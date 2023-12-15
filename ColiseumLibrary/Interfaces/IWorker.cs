using ColiseumLibrary.Contracts.Cards;

namespace ColiseumLibrary.Interfaces;

public interface IWorker
{
    public Task<bool> Work(Deck deck);
}