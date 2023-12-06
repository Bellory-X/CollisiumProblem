using System.Collections.Immutable;
using AutoMapper;
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

    public ICollection<Experiment> GetLatestExperiments(int count)
    {
        return context.ExperimentDbModels
            .Select(mapper.Map<ExperimentDbModel, Experiment>)
            .ToList();
    }

    private bool Save() => context.SaveChanges() > 0;
}