using System;
using System.Threading.Tasks;
using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;
using System.Linq;
using System.Collections.Generic;

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
            return _mapper.Map<CharacterInfoDTO>(await _characterRepo.GetCharacterSimpleAsync(id));
        }

        public async Task<CharacterItemsDTO> GetCharacterItemsAsync(long id)
        {
            return _mapper.Map<CharacterItemsDTO>(await _characterRepo.GetCharacterItemsAsync(id));
        }

        public async Task<IEnumerable<ItemInfoDTO>> GetItemsAsync()
        {
            return _mapper.Map<IEnumerable<ItemInfoDTO>>(await _characterRepo.GetAllItemsAsync());
        }

        public async Task<GuildMembersDTO> GetGuildMembersAsync(long id)
        {
            return _mapper.Map<GuildMembersDTO>(await _characterRepo.GetGuildMembersAsync(id));
        }

        public async Task<IEnumerable<GuildInfoDTO>> GetGuildsAsync()
        {
            return _mapper.Map<IEnumerable<GuildInfoDTO>>(await _characterRepo.GetAllGuildsAsync());
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

        public async Task<Leave_JoinGuildResponseDTO> Leave_JoinGuilddAsyc(Leave_JoinGuildRequestDTO leave_joinRequest)
        {
            var result = new Leave_JoinGuildResponseDTO
            {
                Request = leave_joinRequest
            };

            var character = await _characterRepo.GetCharacterSimpleAsync(leave_joinRequest.CharacterId);

            if (character == null)
            {
                result._result = Leave_JoinGuildResponseDTO.RequestResult.CharacterNotFound;
                return result;
            }

            var guild = await _characterRepo.GetGuildMembersAsync(leave_joinRequest.GuildId);

            if (guild == null)
            {
                result._result = Leave_JoinGuildResponseDTO.RequestResult.GuildNotFound;
                return result;
            }

            var member = guild.Members.FirstOrDefault(m => m.Id == character.Id);

            if (member == null) 
            {
                character.Guild = guild; 
            }
            else
            {
                guild.Members.Remove(member);
            }

            try
            {
                await _characterRepo.SaveChangesAsync();
                result._result = Leave_JoinGuildResponseDTO.RequestResult.Success;

            }
            catch (Exception)
            {
                result._result = Leave_JoinGuildResponseDTO.RequestResult.BadRequest;
            }

            return result;
        }

        public async Task<Un_EquipItemResponseDTO> Un_EquipItemAsync(Un_EquipItemRequestDTO un_equipRequest)
        {
            var result = new Un_EquipItemResponseDTO
            {
                Request = un_equipRequest
            };

            var character = await _characterRepo.GetCharacterItemsAsync(un_equipRequest.CharacterId);

            if(character == null)
            {
                result._result = Un_EquipItemResponseDTO.RequestResult.CharacterNotFound;
                return result;
            }

            var item = character.Items.Where(i => i.Item.Name == un_equipRequest.ItemName).FirstOrDefault();

            if(item == null)
            {
                result._result = Un_EquipItemResponseDTO.RequestResult.InventoryWithoutItem;
                return result;
            }

            
            if (!item.IsEquipped)
            {
                var itemEquipped = character.Items.Where(i => i.Item.Type == item.Item.Type && i.IsEquipped)
                                              .FirstOrDefault();

                if (itemEquipped != null)
                {
                    itemEquipped.IsEquipped = false;
                }
            }

            item.IsEquipped = !item.IsEquipped;

            try
            {
                await _characterRepo.SaveChangesAsync();
                result._result = Un_EquipItemResponseDTO.RequestResult.Success;
              
            }
            catch (Exception)
            {
                result._result = Un_EquipItemResponseDTO.RequestResult.BadRequest;
            }

            return result;
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

                var item = await _itemsRepo.GetItemAsync(purchaseRequest.ItemName);
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

                var characterItem = character.Items.FirstOrDefault(i => i.Item.Name == item.Name);

                if (item.MaxQuantity > 0 && purchase.RequestedQuantity + characterItem?.Quantity > item.MaxQuantity)
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
                        character.Items.Add(new InventoryItem(purchase.Item, purchase.PurchasedQuantity, false));
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