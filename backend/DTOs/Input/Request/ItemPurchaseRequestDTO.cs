using System.Text.Json.Serialization;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class ItemPurchaseRequestDTO : GoldExpenseRequestDTO
    {
        [JsonIgnore]
        public new int ExpenseRequested { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }
    }
}
