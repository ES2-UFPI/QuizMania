using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Profiles
{
    public class GoldExpenseProfile : Profile
    {
        public GoldExpenseProfile()
        {
            CreateMap<GoldExpense, GoldExpenseRequestResultDTO>();
            CreateMap<GoldExpense, GoldExpenseRequestDTO>();
            CreateMap<GoldExpenseRequestDTO, GoldExpense>();
        }
    }
}