using System;
using System.Threading.Tasks;
using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Services
{
    public class CharacterService : ICharacterService
    {
        public const float LevelExperienceConst = 0.14f;

        private readonly ICharacterAsyncRepository _characterRepo;
        private readonly IMapper _mapper;

        public CharacterService(ICharacterAsyncRepository characterRepo, IMapper mapper)
        {
            _characterRepo = characterRepo;
            _mapper = mapper;
        }

        public async Task<CharacterInfoDTO> GetCharacterInfoAsync(long id)
        {
            return _mapper.Map<CharacterInfoDTO>(await _characterRepo.GetCharacterAsync(id));
        }

        public static int GetCurrentLevel(int totalXP)
        {
            return (int)Math.Floor(LevelExperienceConst * Math.Sqrt(totalXP)) + 1;
        }

        public static int GetExperienceSinceLevel(int totalXP, int level)
        {
            return Math.Max(0, (int)(totalXP - Math.Pow((level - 1) / LevelExperienceConst, 2)));
        }
    }
}