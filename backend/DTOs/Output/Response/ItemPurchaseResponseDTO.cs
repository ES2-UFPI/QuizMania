using System;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class ItemPurchaseResponseDTO : GoldExpenseResponseDTO
    {
        public ItemInfoDTO Item { get; set; }
        public int RequestedQuantity { get; set; }
        public int PurchasedQuantity { get; set; }
    }
}
