using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Services
{
    public interface ICharacterService
    {
        Task<CharacterInfoDTO> GetCharacterInfoAsync(long id);
        Task<CharacterItemsDTO> GetCharacterItemsAsync(long id);
        Task<SaveQuizFeedbackResponseDTO.RequestResult> SaveQuizfeedback(QuizFeedback quizFeedback);
        Task<GoldExpenseResponseDTO> TryExpendGold(GoldExpenseRequestDTO expenseRequest);
        Task<ItemPurchaseResponseDTO> TryPurchaseItem(ItemPurchaseRequestDTO purchaseRequest);
    }
}
