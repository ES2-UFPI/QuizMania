using System;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class ItemPurchaseResponseDTO : GoldExpenseResponseDTO
    {
        public string ItemName { get; set; }
        public int RequestedQuantity { get; set; }
        public int PurchasedQuantity { get; set; }
    }
}
