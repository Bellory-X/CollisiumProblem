using System.Collections.Immutable;
using ColiseumLibrary.Model.Cards;

namespace ColiseumLibrary.Model.Experiments;

public record Experiment(int Id, ImmutableArray<Card> Cards, bool? Output);