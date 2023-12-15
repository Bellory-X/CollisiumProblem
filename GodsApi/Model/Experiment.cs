using System.Collections.Immutable;
using ColiseumLibrary.Contracts.Cards;

namespace GodsApi.Model;

public record Experiment(int Id, ImmutableArray<Card> Cards, bool? Output);