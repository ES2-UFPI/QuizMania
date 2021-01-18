using System;
using System.Text.Json.Serialization;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class GoldExpenseResponseDTO
    {
        public long CharacterId { get; set; }

        public DateTime ResquestTime { get; set; }

        public int ExpenseRequested { get; set; }

        [JsonIgnore]
        public GoldExpenseResult Result { get; set; }

        public string ResultMessage => Result.ToString();

        public int RemainingGold { get; set; }
    }
}
