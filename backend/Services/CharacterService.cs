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

        public async Task<bool> SaveQuizfeedback(QuizFeedback quizFeedback)
        {
            var character = await _characterRepo.GetCharacterAsync(quizFeedback.Character.Id);

            if (character == null)
                return false;

            quizFeedback.Character = character;
            character.QuizFeedbacks.Add(quizFeedback);
            
            if(character.QuizFeedbacks.Where(c => c.Quiz.Id == quizFeedback.Quiz.Id).Count() == 1)
            {
                quizFeedback.ExperienceGained = (int)quizFeedback.PercentageOfCorrectChosenAnswers;
                quizFeedback.GoldGained = (int)quizFeedback.PercentageOfCorrectChosenAnswers;

                var prevLevel = character.Level;

                character.TotalXP += quizFeedback.ExperienceGained;
                character.Gold += quizFeedback.GoldGained;

                quizFeedback.LevelGained = character.Level - prevLevel;
            }

            await _characterRepo.SaveChangesAsync();

            return true;
        }

        public async Task<GoldExpenseRequestResultDTO> TryExpendGold(GoldExpenseRequestDTO expenseRequest)
        {
            var character = await _characterRepo.GetCharacterAsync(expenseRequest.CharacterId);

            if (character == null)
                return null;

            var expense = _mapper.Map<GoldExpense>(expenseRequest);

            expense.ResquestTime = DateTime.Now;
            expense.ExpenseAuthorized = character.Gold >= expenseRequest.ExpenseRequested;

            if (expense.ExpenseAuthorized)
            {
                character.Gold -= expenseRequest.ExpenseRequested;
            }

            expense.RemainingGold = character.Gold;

            await _characterRepo.SaveChangesAsync();

            return _mapper.Map<GoldExpenseRequestResultDTO>(expense);
        }
    }
}