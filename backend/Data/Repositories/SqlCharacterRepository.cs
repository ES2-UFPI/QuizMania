using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class SqlCharacterRepository : ICharacterAsyncRepository
    {
        private readonly CharacterContext _characterContext;

        public SqlCharacterRepository(CharacterContext characterContext)
        {
            _characterContext = characterContext;
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _characterContext.Characters.ToListAsync();
        }

        public async Task<Character> GetCharacterAsync(long id)
        {
            return await _characterContext.Characters.FindAsync(id);
        }
    }
}
