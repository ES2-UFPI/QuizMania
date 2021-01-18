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
        private readonly IItemAsyncRepository _itemsRepo;
        private readonly IMapper _mapper;

        public CharacterService(ICharacterAsyncRepository characterRepo, IItemAsyncRepository itemsRepo, IMapper mapper)
        {
            _characterRepo = characterRepo;
            _itemsRepo = itemsRepo;
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

            if (character.QuizFeedbacks.Where(c => c.Quiz.Id == quizFeedback.Quiz.Id).Count() == 1)
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

            expense = await TryExpendGoldInternal(expense);

            return _mapper.Map<GoldExpenseResponseDTO>(expense);
        }

        public async Task<ItemPurchaseResponseDTO> TryPurchaseItem(ItemPurchaseRequestDTO purchaseRequest)
        {
            var purchase = new ItemPurchase(purchaseRequest);

            do
            {
                if (purchaseRequest.Quantity <= 0)
                {
                    purchase.Result = GoldExpenseResult.BadRequest;
                    break;
                }

                var item = await _itemsRepo.GetItemAsync(purchaseRequest.ItemId);
                if (item == null || item.MaxQuantity == 0)
                {
                    purchase.Result = GoldExpenseResult.ItemNotFound;
                    break;
                }

                purchase.Item = item;
                purchase.ExpenseRequested = item.Cost * purchase.RequestedQuantity;

                var character = await _characterRepo.GetCharacterItemsAsync(purchase.CharacterId);
                if (character == null)
                {
                    purchase.Result = GoldExpenseResult.CharacterNotFound;
                    break;
                }

                var characterItem = character.Items.FirstOrDefault(i => i.Item.Id == item.Id);

                if (item.MaxQuantity > 0 && characterItem?.Quantity >= item.MaxQuantity)
                {
                    purchase.Result = GoldExpenseResult.ReachedItemMaxQuantity;
                    break;
                }

                purchase = (ItemPurchase)await TryExpendGoldInternal(purchase, saveChanges: false);

                if (purchase.Result == GoldExpenseResult.Authorized)
                {
                    purchase.Item = item;
                    purchase.PurchasedQuantity = purchase.RequestedQuantity;

                    if (characterItem == null)
                    {
                        character.Items.Add(new ItemQuantity(purchase.Item, purchase.PurchasedQuantity));
                    }
                    else
                    {
                        characterItem.Quantity += purchase.PurchasedQuantity;
                    }

                    await _characterRepo.SaveChangesAsync();
                }
            } while (false);

            return _mapper.Map<ItemPurchaseResponseDTO>(purchase);
        }

        private async Task<GoldExpense> TryExpendGoldInternal(GoldExpense expense, bool saveChanges = true)
        {
            var character = await _characterRepo.GetCharacterSimpleAsync(expense.CharacterId);

            if (character == null)
            {
                expense.Result = GoldExpenseResult.CharacterNotFound;
            }
            else
            {
                if (expense.ExpenseRequested < 0)
                {
                    expense.Result = GoldExpenseResult.BadRequest;
                }
                else if (character.Gold < expense.ExpenseRequested)
                {
                    expense.Result = GoldExpenseResult.NotEnoughResources;
                }
                else
                {
                    expense.Result = GoldExpenseResult.Authorized;
                    character.Gold -= expense.ExpenseRequested;

                    if (saveChanges)
                    {
                        await _characterRepo.SaveChangesAsync();
                    }
                }

                expense.RemainingGold = character.Gold;
            }

            return expense;
        }
    }
}