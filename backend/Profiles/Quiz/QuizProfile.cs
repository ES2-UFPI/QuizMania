using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Profiles
{
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizReadDTO>();
            CreateMap<SaveQuiz_QuizDTO, Quiz>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
