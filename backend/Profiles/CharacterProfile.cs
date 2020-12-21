using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(dest => dest.CurrentLevelXP, opt => opt.MapFrom<CharacterReadDTO.CurrentXPResolver>());
        }
    }
}
