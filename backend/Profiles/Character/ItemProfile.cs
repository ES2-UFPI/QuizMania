using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, long>().ConvertUsing(i => i.Id);
            CreateMap<Item, ItemInfoDTO>();
        }
    }
}
