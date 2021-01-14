using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public interface ICharacterAsyncRepository
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync();
        Task<Character> GetCharacterAsync(long id);
        Task<Character> GetOnlyCharacterAsync(long id);
        Task SaveChangesAsync();
    }
}
