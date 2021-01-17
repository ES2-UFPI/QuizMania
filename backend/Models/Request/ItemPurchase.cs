using System.ComponentModel.DataAnnotations;
using QuizMania.WebAPI.DTOs.Input;

namespace QuizMania.WebAPI.Models
{
    public class ItemPurchase : GoldExpense
    {
        public ItemPurchase() : base() { }

        public ItemPurchase(ItemPurchaseRequestDTO purchaseRequest) : base(purchaseRequest)
        {
            RequestedQuantity = purchaseRequest.Quantity;
        }

        [Required]
        public ItemInfo Item { get; set; }

        [Required]
        public int RequestedQuantity { get; set; }

        [Required]
        public int PurchasedQuantity { get; set; }
    }
}
