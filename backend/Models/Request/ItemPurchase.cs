using System.ComponentModel.DataAnnotations;
using QuizMania.WebAPI.DTOs.Input;

namespace QuizMania.WebAPI.Models
{
    public class ItemPurchase : GoldExpense
    {
        public ItemPurchase() : base() { }

        public ItemPurchase(ItemPurchaseRequestDTO purchaseRequest) : base(purchaseRequest) { }

        [Required]
        public ItemInfo PurchasedItem { get; set; }
    }
}
