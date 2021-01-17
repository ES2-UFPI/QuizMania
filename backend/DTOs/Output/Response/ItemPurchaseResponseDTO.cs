using System;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class ItemPurchaseResponseDTO : GoldExpenseResponseDTO
    {
        public ItemInfoDTO PurchasedItem { get; set; }
    }
}
