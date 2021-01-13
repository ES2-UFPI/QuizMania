using System;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class GoldExpense
    {
        [Key]
        public long Id { get; set; }
        
        [Required]
        public long CharacterId { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime ResquestTime { get; set; }

        [Required]
        public int ExpenseRequested { get; set; }

        [Required]
        public bool ExpenseAuthorized { get; set; }

        [Required]
        public int RemainingGold { get; set; }
    }
}
