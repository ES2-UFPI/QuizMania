using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Profiles
{
    public class ItemQuantityProfile : Profile
    {
        public ItemQuantityProfile()
        {
            CreateMap<ItemQuantity, ItemQuantityDTO>();
        }
    }
}
