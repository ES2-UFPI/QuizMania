using System;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class GoldExpenseResponseDTO
    {
        public long CharacterId { get; set; }

        public DateTime ResquestTime { get; set; }

        public int ExpenseRequested { get; set; }

        public bool ExpenseAuthorized { get; set; }

        public int RemainingGold { get; set; }
    }
}
