using System.Collections.Immutable;
using AutoMapper;
using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Experiments;
using Newtonsoft.Json;

namespace ColiseumLibrary.Data;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Experiment, ExperimentDb>()
            .ForMember("CardColors", opt =>
                opt.MapFrom(src => Convert(src.Cards)));
        CreateMap<ExperimentDb, Experiment>()
            .ForMember("Cards",opt =>
                opt.MapFrom(src => Convert(src.CardColors)));
    }
    
    private static string Convert(ImmutableArray<Card> domainModel) =>
        JsonConvert.SerializeObject(domainModel);
    
    private static ImmutableArray<Card> Convert(string dbModel) =>
        JsonConvert.DeserializeObject<ImmutableArray<Card>>(dbModel);
}