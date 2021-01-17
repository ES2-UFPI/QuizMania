using System;
using System.Threading.Tasks;
using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;
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
            return _mapper.Map<CharacterInfoDTO>(await _characterRepo.GetCharacterAllAsync(id));
        } 

        public async Task<CharacterItemsDTO> GetCharacterItemsAsync(long id)
        {
            return _mapper.Map<CharacterItemsDTO>(await _characterRepo.GetCharacterItemsAsync(id));
        }

        public async Task<SaveQuizFeedbackResponseDTO.RequestResult> SaveQuizfeedback(QuizFeedback quizFeedback)
        {
            var character = await _characterRepo.GetCharacterAllAsync(quizFeedback.Character.Id);

            if (character == null)
                return SaveQuizFeedbackResponseDTO.RequestResult.CharacterNotFound;

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

            try
            {
                await _characterRepo.SaveChangesAsync();
                return SaveQuizFeedbackResponseDTO.RequestResult.Success;
            }
            catch (Exception)
            {
                return SaveQuizFeedbackResponseDTO.RequestResult.BadRequest;
            }
        }

        public async Task<GoldExpenseResponseDTO> TryExpendGold(GoldExpenseRequestDTO expenseRequest)
        {
            var expense = new GoldExpense(expenseRequest);
            var character = await _characterRepo.GetCharacterSimpleAsync(expenseRequest.CharacterId);

            if (character == null)
            {
                expense.Result = GoldExpenseResult.CharacterNotFound;
            }
            else
            {
                if (expenseRequest.ExpenseRequested < 0)
                {
                    expense.Result = GoldExpenseResult.BadRequest;
                }
                else if (character.Gold < expenseRequest.ExpenseRequested)
                {
                    expense.Result = GoldExpenseResult.NotEnoughResources;
                }
                else
                {
                    expense.Result = GoldExpenseResult.Authorized;
                    character.Gold -= expenseRequest.ExpenseRequested;
                }

                expense.RemainingGold = character.Gold;
            }

            await _characterRepo.SaveChangesAsync();

            return _mapper.Map<GoldExpenseResponseDTO>(expense);
        }
    }
}