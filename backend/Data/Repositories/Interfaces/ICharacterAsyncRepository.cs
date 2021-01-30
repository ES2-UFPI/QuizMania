using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public interface ICharacterAsyncRepository
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync();
        Task<Character> GetCharacterAllAsync(long id);
        Task<Character> GetCharacterSimpleAsync(long id);
        Task<Character> GetCharacterItemsAsync(long id);
        Task<IEnumerable<Guild>> GetAllGuildsAsync();
        Task<Guild> GetGuildMembersAsync(long id);
        Task<IEnumerable<ItemInfo>> GetAllItemsAsync();
        Task SaveChangesAsync();
    }
}
