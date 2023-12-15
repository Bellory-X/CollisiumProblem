using System.Collections.Immutable;
using System.Data;
using AutoMapper;
using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Experiments;

namespace GodsApi.Data;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Experiment, ExperimentDbModel>()
            .ForAllMembers(opt =>
                opt.MapFrom(src => Convert(src)));
        CreateMap<ExperimentDbModel, Experiment>()
            .ForAllMembers(opt =>
                opt.MapFrom(src => Convert(src)));
    }

    private static ExperimentDbModel Convert(Experiment domainModel) => 
        new() { Id = domainModel.Id, CardColors = Convert(domainModel.Cards), Output = domainModel.Output };
    
    private static string Convert(ImmutableArray<Card> domainModel) =>
        String.Join('\n', domainModel.Select(x => x.ToString()));
    
    private static Experiment Convert(ExperimentDbModel dbModel) => 
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