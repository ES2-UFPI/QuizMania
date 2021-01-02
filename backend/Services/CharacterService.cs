using System;
using System.Threading.Tasks;
using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;
using System.Linq;

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

        public async Task<bool> SaveQuizfeedback(QuizFeedback quizFeedback, long characterId)
        {
            var character = await _characterRepo.GetCharacterAsync(characterId);

            if (character == null)
                return false;

            character.QuizFeedbacks.Add(quizFeedback);
            
            if(character.QuizFeedbacks.Where(q => q.Quiz.Id == quizFeedback.Quiz.Id).Count() == 1)
            {
                quizFeedback.ExperienceGained = (int)quizFeedback.PercentageOfCorrectChosenAnswers;
                quizFeedback.GoldGained = (int)quizFeedback.PercentageOfCorrectChosenAnswers;

                character.TotalXP += quizFeedback.ExperienceGained;
                character.Gold += quizFeedback.GoldGained;
            }

            await _characterRepo.SaveChangesAsync();

            return true;
        }
    }
}