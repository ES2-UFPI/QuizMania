using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Services
{
    public interface ICharacterService
    {
        Task<CharacterInfoDTO> GetCharacterInfoAsync(long id);
    }
}
