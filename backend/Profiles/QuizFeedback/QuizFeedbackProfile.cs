using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Profiles
{
    public class QuizFeedbackProfile : Profile
    {
        public QuizFeedbackProfile()
        {
            CreateMap<QuizFeedback, QuizFeedbackReadDTO>();
        }
    }
}
