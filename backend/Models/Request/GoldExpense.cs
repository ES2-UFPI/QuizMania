using System;
using System.ComponentModel.DataAnnotations;
using QuizMania.WebAPI.DTOs.Input;

namespace QuizMania.WebAPI
{
    public enum GoldExpenseResult
    {
        BadRequest,
        Authorized,
        CharacterNotFound,
        NotEnoughResources,
        ItemNotFound,
        ReachedItemMaxQuantity,
    }
}

namespace QuizMania.WebAPI.Models
{
    public class GoldExpense
    {
        public GoldExpense() { }

        public GoldExpense(GoldExpenseRequestDTO expenseRequest)
        {
            CharacterId = expenseRequest.CharacterId;
            ExpenseRequested = expenseRequest.ExpenseRequested;
            ResquestTime = DateTime.Now;
        }

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
        public GoldExpenseResult Result { get; set; }

        [Required]
        public int RemainingGold { get; set; }
    }
}
