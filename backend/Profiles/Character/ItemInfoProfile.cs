using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Profiles
{
    public class ItemInfoProfile : Profile
    {
        public ItemInfoProfile()
        {
            CreateMap<ItemInfo, long>().ConvertUsing(i => i.Id);
            CreateMap<ItemInfo, ItemInfoDTO>();
        }
    }
}
