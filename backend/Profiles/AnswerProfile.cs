using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Profiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, long>().ConvertUsing(c => c.Id);
            CreateMap<Answer, AnswerReadDTO>();
        }
    }
}
