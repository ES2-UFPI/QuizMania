using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;


namespace QuizMania.WebAPI.Services
{
    public interface ICharacterService
    {
        Task<CharacterInfoDTO> GetCharacterInfoAsync(long id);
        Task<bool> SaveQuizfeedback(QuizFeedback quizFeedback, long characterId);
    }
}
