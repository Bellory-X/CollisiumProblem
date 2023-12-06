using ColiseumLibrary.Contracts.Cards;

namespace GodsApi.Models;

public record Experiment(int Id, Card[] PlayerCards, Card[] OpponentCards, bool Output);