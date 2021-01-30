using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterInfoDTO>();
            CreateMap<Character, CharacterInfoRankDTO>();
            CreateMap<Character, CharacterItemsDTO>().ForMember(dest => dest.CharacterId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Character, long>().ConvertUsing(c => c.Id);
        }
    }
}
