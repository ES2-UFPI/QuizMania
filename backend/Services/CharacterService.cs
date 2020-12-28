using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI.Services
{
    public class CharacterService : ICharacterService
    {
        public const float LevelExperienceConst = 0.14f;

        private readonly ICharacterAsyncRepository _characterRepo;

        public CharacterService(ICharacterAsyncRepository characterRepo)
        {
            _characterRepo = characterRepo;
        }

        public static int GetCurrentLevel(int totalXP)
        {
            return (int)Math.Floor(LevelExperienceConst * Math.Sqrt(totalXP)) + 1;
        }

        public static int GetExperienceSinceLevel(int totalXP, int level)
        {
            return Math.Max(0, (int)(totalXP - Math.Pow((level - 1) / LevelExperienceConst, 2)));
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _characterRepo.GetAllCharactersAsync();
        }

        public async Task<Character> GetCharacterAsync(long id)
        {
            return await _characterRepo.GetCharacterAsync(id);
        }
    }
}