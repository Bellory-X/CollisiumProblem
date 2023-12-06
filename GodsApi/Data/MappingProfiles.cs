using System.Data;
using AutoMapper;
using ColiseumLibrary.Contracts.Cards;
using GodsApi.Models;

namespace GodsApi.Data;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Experiment, ExperimentDbModel>()
            .ForMember(dest => dest.PlayerColors,
                opt =>
                    opt.MapFrom(src => Convert(src.PlayerCards)))
            .ForMember(dest => dest.OpponentColors,
                opt =>
                    opt.MapFrom(src => Convert(src.OpponentCards)));
        CreateMap<ExperimentDbModel, Experiment>().ForAllMembers(opt =>
                opt.MapFrom(src => 
                    new Experiment(src.Id, 
                        Convert(src.PlayerColors), 
                        Convert(src.OpponentColors), 
                        src.Output)));
    }
    
    private static string Convert(Card[] cards)
    {
        return String.Join('\n', Array.ConvertAll(cards, el => el.ToString()));
    }

    private static Card[] Convert(string str)
    {
        var colors = str.Split('\n');
        if (colors.Length != 18)
        {
            throw new DataException("colors not equals 18");
        }

        return Array.ConvertAll(colors, s => {
            return s switch 
            { 
                "♠️" => new Card(CardColor.Black), 
                "♦️" => new Card(CardColor.Red), 
                _ => throw new DataException("color not exist"), 
            };
        });
    }
}