using System.Collections.Immutable;
using System.Data;
using AutoMapper;
using ColiseumLibrary.Contracts.Cards;
using GodsApi.Data;
using GodsApi.Models;
using Microsoft.Extensions.Logging;

namespace GodsApi.Repository;

public class ExperimentRepository(
    ILogger<IExperimentRepository> logger, 
    ApplicationDbContext context, 
    IMapper mapper
    ) : IExperimentRepository
{
    public bool AddExperiments(ImmutableList<Experiment> domainModels)
    {
        var dbModels = domainModels.Select(mapper.Map<Experiment, ExperimentDbModel>);
        context.AddRange(dbModels);
        return Save();
    }

    public bool DeleteAllExperiments(ImmutableList<Experiment> domainModels)
    {
        context.RemoveRange(domainModels.Select(mapper.Map<Experiment, ExperimentDbModel>));
        return Save();
    }

    public List<Experiment> GetLatestExperiments(int count)
    {
        return context.ExperimentDbModels.Select(el =>
                new Experiment(el.Id, Convert(el.CardColors), el.Output))
            .ToList();
    }
    
    private static ImmutableArray<Card> Convert(string dbModel)
    {
        var domainModel = dbModel.Split('\n');
        if (domainModel.Length != 36) throw new DataException("colors not equals 36");

        return Array.ConvertAll(domainModel, s => {
            return s switch 
            { 
                "♠️" => new Card(CardColor.Black), 
                "♦️" => new Card(CardColor.Red), 
                _ => throw new DataException("color not exist"), 
            };
        }).ToImmutableArray();
    }

    private bool Save() => context.SaveChanges() > 0;
}