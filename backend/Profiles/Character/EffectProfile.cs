using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Profiles
{
    public class EffectBaseProfile : Profile
    {
        public EffectBaseProfile()
        {
            CreateMap<EffectBase, EffectBaseInfoDTO>();
        }
    }
}
