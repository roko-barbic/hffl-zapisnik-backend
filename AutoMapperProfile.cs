namespace kompaApp.Application.Helpers;

using AutoMapper;
using roko_test.DTO;
using roko_test.Entities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
       CreateMap<Tournament, TournamentDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

      
    }
}