using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionReadDTO>()
                .ForMember(dest => dest.HasMultipleCorrectAnswers, opt => opt.MapFrom<QuestionReadDTO.MultipleCorrectAnswersResolver>());
        }
    }
}
