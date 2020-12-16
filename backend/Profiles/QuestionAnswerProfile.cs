using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Profiles
{
    public class QuestionAnswerProfile : Profile
    {
        public QuestionAnswerProfile()
        {
            CreateMap<QuestionAnswerDTO, QuestionAnswer>();
            CreateMap<QuestionAnswer, QuestionAnswerDTO>();
        }
    }
}
