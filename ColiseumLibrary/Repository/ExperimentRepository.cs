﻿using System.Collections.Immutable;
using System.Data;
using AutoMapper;
using ColiseumLibrary.Data;
using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Experiments;

namespace ColiseumLibrary.Repository;

public class ExperimentRepository(ExperimentDbContext context) : IExperimentRepository
{
    public bool AddExperiment(Experiment domainModel)
    {
        AddExperimentToContext(domainModel);
        return Save();
    }
    
    public Experiment? GetExperimentById(int id)
    {
        var dbModel = context.ExperimentDbs.Find(id);
        return dbModel is null ? null : Convert(dbModel);
    }
    
    public bool AddExperiments(ImmutableList<Experiment> domainModels)
    {
        foreach (var domainModel in domainModels)
        {
            AddExperimentToContext(domainModel);
        }
        return Save();
    }

    public List<Experiment> GetExperiments(int count) => 
        context.ExperimentDbs.OrderByDescending(s => s.Id)
            .Select(Convert).Take(count).ToList();
    
    private void AddExperimentToContext(Experiment domainModel)
    {
        var dbModel = context.ExperimentDbs.Find(domainModel.Id);
        if (dbModel is null)
        {
            context.ExperimentDbs.Add(Convert(domainModel));
        }
        else
        {
            dbModel.CardColors = Convert(domainModel.Cards);
            dbModel.Output = domainModel.Output;
        }
    }
    
    private bool Save() => context.SaveChanges() > 0;
    
    private static ExperimentDb Convert(Experiment domainModel) => 
        new() { Id = domainModel.Id, CardColors = Convert(domainModel.Cards), Output = domainModel.Output };
    
    private static string Convert(ImmutableArray<Card> domainModel) =>
        String.Join('\n', domainModel.Select(x => x.ToString()));
    
    private static Experiment Convert(ExperimentDb dbModel) => 
        new(dbModel.Id, Convert(dbModel.CardColors), dbModel.Output);
    
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
}