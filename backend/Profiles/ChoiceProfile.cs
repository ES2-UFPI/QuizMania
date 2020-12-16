using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Profiles
{
    public class ChoiceProfile : Profile
    {
        public ChoiceProfile()
        {
            CreateMap<Choice, ChoiceReadDTO>();
        }
    }
}
