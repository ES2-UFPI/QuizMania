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
        Task<IEnumerable<ItemInfoDTO>> GetItemsAsync();
        Task<GuildMembersDTO> GetGuildMembersAsync(long id);
        Task<IEnumerable<GuildInfoDTO>> GetGuildsAsync();
        Task<SaveQuizFeedbackResponseDTO.RequestResult> SaveQuizfeedback(QuizFeedback quizFeedback);
        Task<Leave_JoinGuildResponseDTO> Leave_JoinGuilddAsyc(Leave_JoinGuildRequestDTO leave_joinRequest);
        Task<Un_EquipItemResponseDTO> Un_EquipItemAsync(Un_EquipItemRequestDTO un_equipRequest);
        Task<GoldExpenseResponseDTO> TryExpendGold(GoldExpenseRequestDTO expenseRequest);
        Task<ItemPurchaseResponseDTO> TryPurchaseItem(ItemPurchaseRequestDTO purchaseRequest);
        Task<CharacterRankingDTO> GetRanking(int guildId);
    }
}
