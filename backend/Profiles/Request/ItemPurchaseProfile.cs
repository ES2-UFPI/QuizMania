using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Profiles
{
    public class ItemPurchaseProfile : GoldExpenseProfile
    {
        public ItemPurchaseProfile()
        {
            CreateMap<ItemPurchase, ItemPurchaseResponseDTO>();
            CreateMap<ItemPurchaseRequestDTO, ItemPurchase>();
        }
    }
}