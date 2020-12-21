using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class MockCharacterRepository : ICharacterAsyncRepository
    {
        private readonly CharacterContext _characterContext;

        public MockCharacterRepository(CharacterContext characterContext)
        {
            _characterContext = characterContext;

            // mock characters
            _characterContext.Add(new Character()
            {
                Id = 1,
                Name = "Gandalf",
                Level = 1,
                TotalXP = 5,
                Gold = 10,
                HealthPoints = 100,
            });

            _characterContext.Add(new Character()
            {
                Id = 2,
                Name = "Jurema",
                Level = 2,
                TotalXP = 55,
                Gold = 70,
                HealthPoints = 80,
            });

            _characterContext.SaveChangesAsync();
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