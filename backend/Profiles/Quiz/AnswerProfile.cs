using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Profiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, long>().ConvertUsing(c => c.Id);
            CreateMap<Answer, AnswerReadDTO>();
            CreateMap<SaveQuiz_AnswerDTO, Answer>();
            CreateMap<SaveQuestion_AnswerDTO, Answer>();
        }
    }
}
