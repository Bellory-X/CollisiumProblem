using System.Collections.Immutable;
using System.Data;
using AutoMapper;
using ColiseumLibrary.Contracts.Cards;
using GodsApi.Model;

namespace GodsApi.Data;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Experiment, ExperimentDbModel>()
            .ForMember(dest => dest.CardColors,
                opt => 
                    opt.MapFrom(src => Convert(src.Cards)));
        CreateMap<ExperimentDbModel, Experiment>()
            .ForAllMembers(opt =>
                opt.MapFrom(src => 
                    new Experiment(src.Id, Convert(src.CardColors), src.Output)));
    }

    private static string Convert(ImmutableArray<Card> domainModel) =>
        String.Join('\n', domainModel.Select(x => x.ToString()));
    
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