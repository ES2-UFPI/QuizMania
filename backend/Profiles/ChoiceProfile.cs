using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Profiles
{
    public class ChoiceProfile : Profile
    {
        public ChoiceProfile()
        {
            CreateMap<Choice, long>().ConvertUsing(c => c.Id);
            CreateMap<Choice, ChoiceReadDTO>();
        }
    }
}
