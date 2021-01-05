using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

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
