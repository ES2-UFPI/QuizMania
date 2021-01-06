using System;

namespace QuizMania.WebAPI.DTOs
{
    public class GoldExpenseRequestResultDTO
    {
        public long CharacterId { get; set; }

        public DateTime ResquestTime { get; set; }

        public int ExpenseRequested { get; set; }

        public bool ExpenseAuthorized { get; set; }

        public int RemainingGold { get; set; }
    }
}
